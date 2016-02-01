/////////////////////////////
//dyncaller.cpp
//
//
//Call a DLL at the run time 
//
// 
//

#include <stdio.h>
#include <windows.h>

///////////////////
//
// typedef for Binary Function ( Add(int , int ) )
//
//

typedef int (__stdcall * BinaryFunc ) ( int , int );


int main( int argc , char **argv )
{

   HMODULE h = LoadLibrary("test.dll");


   if ( h == INVALID_HANDLE_VALUE ) {

	fprintf(stdout,"Failed to load the DLL\n");
	return -1;       
   }


   BinaryFunc bfn = (BinaryFunc)GetProcAddress(h,"Add");

   if ( bfn == 0 ) {

       fprintf(stdout,"Failed to retrive func pointer\n");
       return -2;
   }
  
   printf("The value is %d\n",(*bfn)(2,3));

   FreeLibrary(h);

   return 0;

}
