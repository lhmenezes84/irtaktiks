/*  reacTIVision fiducial tracking framework
    FidtrackFinderClassic.h
    Copyright (C) 2006 Martin Kaltenbrunner <mkalten@iua.upf.es>

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

#ifndef FIDTRACKFINDERCLASSIC
#define FIDTRACKFINDERCLASSIC

#include "FiducialFinder.h"
#include "segment.h"
#include "fidtrack120.h"
#define MAX_FIDUCIAL120_COUNT 120

class FidtrackFinderClassic: public FiducialFinder
{
public:	
	FidtrackFinderClassic(MessageServer *server, char* grid_cfg) : FiducialFinder (server,grid_cfg) {};
	~FidtrackFinderClassic() {};
	
	void process(unsigned char *src, unsigned char *dest);
	bool init(int w ,int h, int sb, int db);
	
private:
	Segmenter segmenter;
	Fiducial120 fiducials[MAX_FIDUCIAL120_COUNT];
	PartialSegmentTopology partialSegmentTopology;
};

#endif
