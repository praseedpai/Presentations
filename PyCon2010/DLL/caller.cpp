////////////////////////////////////
// caller.cpp
//
// cl caller.cpp test.lib 
//
//
//

#include <stdio.h>

extern "C" int __stdcall Add( int , int );

int main( int argc , char **argv )
{
   printf("The value is %d\n",Add(2,3));

}