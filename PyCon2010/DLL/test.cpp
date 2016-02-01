/////////////////////////////
// test.cpp
//
// A Simple DLL written using C/C++
//
// At the Visual studio command prompt
//
// cl /c test.cpp
//
// link /DLL /out:test.dll /DEF:test.def test.obj
//

#include <stdio.h>

extern "C" __declspec(dllexport) int __stdcall Add(int a , int b ) {

	return a + b;
}