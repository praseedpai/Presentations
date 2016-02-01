////////////////////////////////////////////
//
// Writing a Custom Type for Consumption from
// Python 
// 
//
// Written By Praseed Pai K.T.
//            http://praseedp.blogspot.com
//
//
// for Pyton 3.x
// --------------
// cl /c /D_PYTHON_3X -IC:\Python31\include PythonType.cpp
// link /DLL /out:PythonTypeExt.pyd PythonType.obj 
//       C:\Python31\libs\Python31.lib
//
// for Pyton 2.5.x
// ----------------
// cl /c -IC:\Python25\include PythonType.cpp
// link /DLL /out:PythonTypeExt.pyd PythonType.obj 
//       C:\Python25\libs\Python25.lib
//


#include <Python.h>

////////////////////
//
// for offsetof function
//
//

#include "structmember.h"

#ifdef _PYTHON_3X

#define MEMBER_ATTRIBUTES PyMemberDef
#define PYTHON_STRING_CONVERT(b) PyUnicode_FromString((b))


#else

#define MEMBER_ATTRIBUTES memberlist
#define PYTHON_STRING_CONVERT(b) PyString_FromString((b))

#endif


////////////////////////
//
// Data structure to store 
// an integer and float .
// These will be exposed as
// properties
//

typedef struct {
    PyObject_HEAD
    int vint;
    float vfloat; 
}PyConType;

////////////////////////////////
//
//
// Function prototypes
//
//

static PyObject*
PyConTypeNew(PyObject *self, PyObject *args);

static PyObject*
Spit(PyConType *self, PyObject *args);

static void
pycontype_dealloc(PyConType* cons);

//////////////////////////////////
//
//
// Method supported by this object
//


static PyMethodDef pycontype_methods[  ] = {
    {"Spit", (PyCFunction)Spit, METH_VARARGS
    },    {NULL}  /* Sentinel */
};

//////////////////////////////
//
// Attributes inside the type
//
static struct MEMBER_ATTRIBUTES  pycontype_memberlist[] = {
         {"vint",T_INT,offsetof(PyConType,vint)},
         {"vfloat",T_FLOAT,offsetof(PyConType,vfloat)},
         {NULL}
};

///////////////////////////////////
//
// Method to create a new instance exported
// from the module
//
static PyMethodDef pycon_module_functions[  ] = {
    {"PyConTypeNew",   PyConTypeNew,   METH_VARARGS},
    {0, 0}
};


#ifdef _PYTHON_3X

static PyObject *
pycontype_getvint(PyConType *self, void *closure)
{
   
    return Py_BuildValue("i", self->vint);
}

static void
pycontype_setvint(PyConType *self, PyObject *value ,void *closure)
{
   
     int cnt;

     if ( !PyArg_ParseTuple(value,"i",&cnt) ) {
	      PyErr_SetString(PyExc_RuntimeError,
                  "integer argument expected .." );
              return ;
        }  

     self->vint = cnt;

}

static PyObject *
pycontype_getvfloat(PyConType *self, void *closure)
{
   
    return  Py_BuildValue("d", self->vfloat);
}

static void
pycontype_setvfloat(PyConType *self,PyObject *value, void *closure)
{
   
     double cnt;

     if ( !PyArg_ParseTuple(value,"d",&cnt) ) {
	      PyErr_SetString(PyExc_RuntimeError,
                  "double argument expected .." );
              return ;
        }  

     self->vfloat = cnt;
}




static PyGetSetDef pycontype_getseters[] = {
    {"vint", 
     (getter)pycontype_getvint, (setter)pycontype_setvint,
     0,
     NULL},
    {"vfloat", 
     (getter)pycontype_getvfloat, (setter)pycontype_setvfloat,
     0,
     NULL},
    {NULL}  /* Sentinel */
};


static PyTypeObject pycontype_type = {
    PyVarObject_HEAD_INIT(NULL, 0)
    "pycontype",             /* tp_name */
    sizeof(PyConType),        /* tp_basicsize */
    0,                         /* tp_itemsize */
    (destructor)pycontype_dealloc, /* tp_dealloc */
    0,                         /* tp_print */
    0,
    0,
    0,                         /* tp_reserved */
    0,                         /* tp_repr */
    0,                         /* tp_as_number */
    0,                         /* tp_as_sequence */
    0,                         /* tp_as_mapping */
    0,                         /* tp_hash  */
    0,                         /* tp_call */
    0,                         /* tp_str */
    0,                         /* tp_getattro */
    0,                         /* tp_setattro */
    0,                         /* tp_as_buffer */
    Py_TPFLAGS_DEFAULT |
        Py_TPFLAGS_BASETYPE,   /* tp_flags */
    0,           /* tp_doc */
    0,		               /* tp_traverse */
    0,		               /* tp_clear */
    0,		               /* tp_richcompare */
    0,		               /* tp_weaklistoffset */
    0,		               /* tp_iter */
    0,		               /* tp_iternext */
    pycontype_methods,             /* tp_methods */
    pycontype_memberlist,             /* tp_members */
    pycontype_getseters,                         /* tp_getset */
    0,                         /* tp_base */
    0,                         /* tp_dict */
    0,                         /* tp_descr_get */
    0,                         /* tp_descr_set */
    0,                         /* tp_dictoffset */
    0,      /* tp_init */
    0,                         /* tp_alloc */
    0,                 /* tp_new */
};



#else




static PyObject *
pycontype_getattr(PyConType *self , char *attr )
{

    PyObject *res = 0;

    res = Py_FindMethod(pycontype_methods,(PyObject *)self,attr);

    if ( res == 0 ) {
       PyErr_Clear();
       return PyMember_Get((char *)self ,
                        pycontype_memberlist,attr );
    }
    return res;
}

int
pycontype_setattr(PyConType *self , char *attr,PyObject *value )
{

    if ( value == 0 ) {
           return -1;
    }
    return PyMember_Set((char *)self ,
                        pycontype_memberlist,attr,value );
}





static PyTypeObject pycontype_type = {
    PyObject_HEAD_INIT(0)     /* initialize to 0 to ensure Win32 portability  */
    0,                        /* ob_size */
    "pycontype",                   /* tp_name */
    sizeof(PyConType),        /* tp_basicsize */
    0,                        /* tp_itemsize */
    /* methods */
    (destructor)pycontype_dealloc, /* tp_dealloc */
    0,
    (getattrfunc) pycontype_getattr,
    (setattrfunc) pycontype_setattr
    /* implied by ISO C: all zeros thereafter, i.e., no other method */
};




#endif




static PyObject*
PyConTypeNew(PyObject *self, PyObject *args)
{

    if ( !PyArg_ParseTuple(args,":PyConType"))
                   return 0;

     
    PyConType *cons = PyObject_New(PyConType, &pycontype_type);
    cons->vint = 100;
    cons->vfloat = 3.14159;

    return (PyObject *)cons;
}


static PyObject*
Spit(PyConType *self, PyObject *args)
{

    if ( !PyArg_ParseTuple(args,":PyConType"))
                   return 0;

    char buffer[255];
    sprintf(buffer,"%d %f" , self->vint , self->vfloat );
    return PYTHON_STRING_CONVERT(buffer);

}

static void
pycontype_dealloc(PyConType* cons)
{
    
    PyObject_Del(cons);
}



#ifdef _PYTHON_3X 


static PyModuleDef PyExtModule = {
    PyModuleDef_HEAD_INIT,
    "PythonTypeExt",
    0,
    -1,
    pycon_module_functions,NULL, NULL, NULL, NULL

};

//////////////////////////////////////
//
// Python Interpreter initializes the module by 
// calling the routine given below
//
//


PyMODINIT_FUNC
PyInit_PythonTypeExt(void) 
{
    PyObject* m;

    if (PyType_Ready(&pycontype_type) < 0)
        return NULL;

    m = PyModule_Create(&PyExtModule);
    if (m == NULL)
        return NULL;

    Py_INCREF(&pycontype_type);
    PyModule_AddObject(m, "PythonTypeExt", (PyObject *)&pycontype_type);
    return m;
}


#else 


/* module entry-point (module-initialization) function */
extern "C" void __declspec(dllexport) initPythonTypeExt(void)
{
    /* Create the module, with its functions */
    PyObject *m = Py_InitModule("PythonTypeExt", pycon_module_functions);
    /* Finish initializing the type-objects */
    pycontype_type.ob_type = &PyType_Type;
}

#endif
