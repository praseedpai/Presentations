////////////////////////////////////////////////
//
// Embed.cpp
//
// A Simple program to demonstrate the embedding of 
// Python interpreter inside a C/C++ Program.
//
//
// Written by Praseed Pai K.T.
//            http://praseedp.blogspot.com
//
//
// For Python3.x
// -------------
// cl -IC:\Python31\include Embed.cpp /D_PYTHON_3X 
// C:\Python31\libs\Python31.lib
//
// For Python3.x
// -------------
// cl -IC:\Python25\include Embed.cpp C:\Python25\libs\Python25.lib
//

#include <Python.h>

///////////////////////////////////////
//
// The user entry point for a C/C++ program
//
//
//

int main(int argc, char *argv[])
{


	Py_Initialize();

#ifdef _PYTHON_3X

	PyRun_SimpleString("from time import time,ctime\n"
                     "print('Today is', ctime(time()))\n");
#else
	PyRun_SimpleString("from time import time,ctime\n"
                     "print 'Today is', ctime(time())\n");

#endif 

	Py_Finalize();
	
	return 0;
}

