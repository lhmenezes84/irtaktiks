/*  portVideo, a cross platform camera framework
   Copyright (C) 2005 Martin Kaltenbrunner <mkalten@iua.upf.es>

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

#include "dslibCamera.h"

dslibCamera::dslibCamera()
{

	xml_config = "<?xml version='1.0' encoding='UTF-8'?> <dsvl_input> <camera show_format_dialog='true'><pixel_format><RGB24/></pixel_format></camera></dsvl_input>";

	CoInitialize(NULL);
	dsvl_vs = new DSVL_VideoSource();

	pbuffer = NULL;
	running = false;
}


dslibCamera::~dslibCamera()
{
	if (pbuffer!=NULL) delete []pbuffer;
}

bool dslibCamera::findCamera() {
	return true;
}

bool dslibCamera::initCamera(int width, int height, bool colour) {

	this->width = width;
	this->height = height;
	this->fps = fps;
	this->colour = colour;
	bytes = (colour?3:1);

	LONG w,h;
	double f;

	if(FAILED(dsvl_vs->BuildGraphFromXMLString(xml_config))) return false;
	if(FAILED(dsvl_vs->GetCurrentMediaFormat(&w,&h,&f,NULL))) return false;

	this->width = w;
	this->height = h;
	this->fps = f;

	cameraName = "WDM Camera";

	pbuffer = new unsigned char[this->width*this->height*bytes];
	return true;
}


bool dslibCamera::startCamera()
{ 
	if(FAILED(dsvl_vs->EnableMemoryBuffer())) return(false);
	if(FAILED(dsvl_vs->Run())) return(false);

	running = true;
	return true;
}

unsigned char* dslibCamera::getFrame()
{
		DWORD wait_result = dsvl_vs->WaitForNextSample(/*INFINITE*/1000/fps);

		if(wait_result != WAIT_OBJECT_0) 
		{
			//wait another 1.5 seconds
			wait_result = dsvl_vs->WaitForNextSample(1500);
			if(wait_result != WAIT_OBJECT_0) stopCamera();
		}

		if(wait_result == WAIT_OBJECT_0)
		{

			unsigned char r,g,b;
			int dwidth = 2*width;

			dsvl_vs->CheckoutMemoryBuffer(&g_mbHandle, &buffer);
			g_Timestamp = dsvl_vs->GetCurrentTimestamp();
			switch (colour) {
				case false: {
					unsigned char *src = (unsigned char*)buffer;
					unsigned char *dest = pbuffer;
					dest+=width*height-width;

					for(int y=0;y<height;y++) {
						for(int x=0;x<width;x++) {
							//quicker
							/**dest++ = *src++;
							src++;
							src++;*/

							//better
							/*r = *src++;
							g = *src++;
							b = *src++;
							*dest++ = (unsigned char)(0.30 * r + 0.59 * g + 0.11 * b);*/

							//fastest and best
							r = *src++;
							g = *src++;
							b = *src++;
							*dest++ = hibyte(r * 77 + g * 151 + b * 28);
						}
						dest-=dwidth;
					}
					break;
				}
				case true: {
					return buffer;
					//memcpy(pbuffer,buffer,width*height*bytes);
					break;
				}
			}
			dsvl_vs->CheckinMemoryBuffer(g_mbHandle);
			return pbuffer;
		} 

		return NULL;
}

bool dslibCamera::stopCamera()
{
	dsvl_vs->Stop();
	running = false;
	return true;
}
bool dslibCamera::stillRunning() {
	return running;
}

bool dslibCamera::resetCamera()
{
  return (stopCamera() && startCamera());

}

bool dslibCamera::closeCamera()
{
	if(FAILED(dsvl_vs->DisableMemoryBuffer())) return(false);
	delete dsvl_vs;
	return true;
}

void dslibCamera::showSettingsDialog() {

	dsvl_vs->ShowFilterProperties();
}
