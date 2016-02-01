//////////////////////////
// pycall.cpp
//
// Calling a Python module from a C/C++ program
//
//
// Written by Praseed Pai K.T.
//            http://praseedp.blogspot.com
//
//
// For Python3.x
// -------------
// cl -IC:\Python31\include pycall.cpp /D_PYTHON_3X 
// C:\Python31\libs\Python31.lib
//
// For Python3.x
// -------------
// cl -IC:\Python25\include pycall.cpp C:\Python25\libs\Python25.lib
//

#include <Python.h>


#ifdef _PYTHON_3X

#define PYTHON_STRING_CONVERT(b) PyUnicode_FromString((b))
#define PYTHON_NUMBER_CONVERT(b) PyLong_FromLong((b))
#define PYTHONLONGASLONG(b) PyLong_AsLong((b))
#else

#define PYTHON_STRING_CONVERT(b) PyString_FromString((b))
#define PYTHON_NUMBER_CONVERT(b) PyInt_FromLong((b))
#define PYTHONLONGASLONG(b) PyInt_AsLong((b))

#endif


int
main(int argc, char *argv[])
{
    PyObject *pName, *pModule, *pDict, *pFunc;
    PyObject *pArgs, *pValue;
    int i;

    if (argc < 3) {
        fprintf(stderr,"Usage: call pythonfile funcname [args]\n");
        return 1;
    }

    Py_Initialize();
    pName = PYTHON_STRING_CONVERT(argv[1]);

    /* Error checking of pName left out */

    pModule = PyImport_Import(pName);
    Py_DECREF(pName);

    if (pModule != NULL) {
        pFunc = PyObject_GetAttrString(pModule, argv[2]);
        /* pFunc is a new reference */

        if (pFunc && PyCallable_Check(pFunc)) {
            pArgs = PyTuple_New(argc - 3);
            for (i = 0; i < argc - 3; ++i) {
                pValue = PYTHON_NUMBER_CONVERT(atoi(argv[i + 3]));
                if (!pValue) {
                    Py_DECREF(pArgs);
                    Py_DECREF(pModule);
                    fprintf(stderr, "Cannot convert argument\n");
                    return 1;
                }
                /* pValue reference stolen here: */
                PyTuple_SetItem(pArgs, i, pValue);
            }
            pValue = PyObject_CallObject(pFunc, pArgs);
            Py_DECREF(pArgs);
            if (pValue != NULL) {
                printf("Result of call: %ld\n", PYTHONLONGASLONG(pValue));
                Py_DECREF(pValue);
            }
            else {
                Py_DECREF(pFunc);
                Py_DECREF(pModule);
                PyErr_Print();
                fprintf(stderr,"Call failed\n");
                return 1;
            }
        }
        else {
            if (PyErr_Occurred())
                PyErr_Print();
            fprintf(stderr, "Cannot find function \"%s\"\n", argv[2]);
        }
        Py_XDECREF(pFunc);
        Py_DECREF(pModule);
    }
    else {
        PyErr_Print();
        fprintf(stderr, "Failed to load \"%s\"\n", argv[1]);
        return 1;
    }
    Py_Finalize();
    return 0;
}


