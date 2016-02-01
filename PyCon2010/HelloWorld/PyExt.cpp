///////////////////////////////
//
// PyExt.cpp
//
// A Simple Python Extension under Visual C/C++
//
// Written by Praseed Pai K.T.
//            http://praseedp.blogspot.com
//
//
// At the Visual studio Command prompt
//
// For Python 3.1  => We need to define _PYTHON_3X
// -----------------------------------------------
// The Python path for my machine is C:\Python31 
//
// cl /c -IC:\Python31\include /D_PYTHON_3X PyExt.cpp
// 
// link /DLL /out:PyExt.pyd PyExt.obj C:\Python31\libs\Python31.lib
//
// For Python 2.5 
// -----------------------------------------------
// The Python path for my machine is C:\Python25 
//
// cl /c -IC:\Python25\include  PyExt.cpp
// 
// link /DLL /out:PyExt.pyd PyExt.obj C:\Python25\libs\Python25.lib
//
//

#include <Python.h>
#include <stdlib.h>

/////////////////////////////////
//
// This is the actual routine which returns the string "Hello World.."
//
//
//

static PyObject * SayHello( PyObject *self , PyObject *args )
{
	PyObject *return_value = 0;
	return_value = Py_BuildValue("s","Hello Bengaluru ....");
	return return_value;

}


////////////////////////////////////////
//
// Initialize the PyMethodDef table
//
//
static struct PyMethodDef pyext_methods[] = {
      {"SayHello",SayHello,METH_NOARGS,0},
      { 0 , 0 }

};


#ifdef _PYTHON_3X

///////////////////////////////////
//
// Initialize the table
//
//

static struct PyModuleDef PyExtModule = {
   PyModuleDef_HEAD_INIT,
   "PyExt",   /* name of module */
    0, /* module documentation, may be NULL */
   -1,       /* size of per-interpreter state of the module,
                or -1 if the module keeps state in global variables. */
   pyext_methods
};

//////////////////////////////////////
//
// Python Interpreter initializes the module by 
// calling the routine given below
//
//
PyMODINIT_FUNC  PyInit_PyExt(void) {

 return  PyModule_Create(&PyExtModule);
}


#else

extern "C" __declspec(dllexport) void __stdcall initPyExt() {

   Py_InitModule("PyExt",pyext_methods);
}

#endif

