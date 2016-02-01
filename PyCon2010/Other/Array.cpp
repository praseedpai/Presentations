///////////////////////////////
//
// Array.cpp
//
// A Simple Python Extension under Visual C/C++
//
// Written by Praseed Pai K.T.
//            http://praseedp.blogspot.com
//
//
// At the Visual studio Command prompt
//
// for Python 3.x
// ----------------
//
// The Python path for my machine is C:\Python31 
//
// cl /c -IC:\Python31\include  Array.cpp
// 
// link /DLL /out:ArrayExt.pyd Array.obj H:\Python31\libs\Python31.lib
//
// for Python 2.x
// ----------------
//
// The Python path for my machine is C:\Python25 
//
// cl /c -IC:\Python25\include  Array.cpp
// 
// link /DLL /out:ArrayExt.pyd Array.obj /DEF:ArrayExt.def 
//      C:\Python31\libs\Python31.lib
//

#include <Python.h>
#include <stdlib.h>

/////////////////////////////////
//
// This is the actual routine which returns the string "Hello World.."
//
//
//

static PyObject * SQuare( PyObject *self , PyObject *args )
{

        double cnt=0; 
        if ( !PyArg_ParseTuple(args,"d",&cnt) ) {
	      PyErr_SetString(PyExc_RuntimeError,
                  "double argument expected .." );
              return NULL;
        }  
 
	PyObject *return_value = 0;
	return_value = Py_BuildValue("d",cnt*cnt);
	return return_value;

}
///////////////////////////////////////////
//
//
//
//

static PyObject * IsSolutionForQuad( PyObject *self , PyObject *args )
{

        double a , b , c ; 
        a=b=c=0;
 
        PyArg_ParseTuple(args,"ddd",&a,&b,&c); 

        double disc = b*b - 4*a*c;

        if ( disc < 0.0 ) {
		return Py_BuildValue("i",0);
        } 

        double x1 = ( -b + sqrt(disc) ) / (2*a);
        double x2 = ( -b - sqrt(disc) ) / (2*a); 

             
	PyObject *return_value = 0;
	return_value = Py_BuildValue("dd",x1,x2);
	return return_value;

}

/////////////////////////////////
//
//
//
//
//
static PyObject *SumList( PyObject *self , PyObject *args )
{

   PyObject* myTuple;
   if(!PyArg_ParseTuple(args,"O",&myTuple))
             return NULL;


   if ( !PyList_Check(myTuple) ) {
         PyErr_SetString(PyExc_RuntimeError,
                  "List argument expected .." );
         return NULL;

    }
 
   int length = (int)PyList_Size(myTuple);
   double sum = 0;
   double dbl = 0;
   for(int i=0; i < length; ++i )
   {
       PyObject *next = PyList_GetItem(myTuple,i);

       if ( PyFloat_Check(next) ) { 
          dbl = PyFloat_AsDouble(next);
       }
       sum += dbl;   
   } 

   return Py_BuildValue("d",sum);

}

/////////////////////////////////
//
//
//
//
//
static PyObject *SumTuple( PyObject *self , PyObject *args )
{

   PyObject* myTuple;
   if(!PyArg_ParseTuple(args,"O",&myTuple))
             return NULL;


   if ( !PyTuple_Check(myTuple) ) {

         PyErr_SetString(PyExc_RuntimeError,
                  "Tuple argument expected .." );
         return NULL;

    }
 
   int length = (int)PyTuple_Size(myTuple);
   double sum = 0;
   double dbl = 0;
   for(int i=0; i < length; ++i )
   {
       PyObject *next = PyTuple_GetItem(myTuple,i);

       if ( PyFloat_Check(next) ) { 
          dbl = PyFloat_AsDouble(next);
          
       }
         
       sum += dbl;   
   } 

   return Py_BuildValue("d",sum);

}

////////////////////////////////////////
//
// Initialize the PyMethodDef table
//
//
static struct PyMethodDef pyext_methods[] = {
      {"SQuare",SQuare,METH_VARARGS,0},
      {"IsSolutionForQuad",IsSolutionForQuad,METH_VARARGS,0},
      {"SumList",SumList,METH_VARARGS,0},
      {"SumTuple",SumTuple,METH_VARARGS,0},
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
   "ArrayExt",   /* name of module */
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
PyMODINIT_FUNC  PyInit_ArrayExt(void) {

 return  PyModule_Create(&PyExtModule);
}


#else
extern "C" 
 __declspec(dllexport) void __stdcall initArrayExt()
{

     Py_InitModule("ArrayExt",pyext_methods);

}



#endif


