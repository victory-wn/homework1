#include<iostream>   
#include<time.h>  
using namespace std;  
int sta=1;  
int var=0;  
void end_game()  
{  
    {cout<<"�����ˣ�"<<"�����㣬��ȷ����"<<var<<endl;}  
}  
void conti()  
{  
    cout<<"����һ��? ��(����y) or ��(��������):\n";  
    char temp;  
    cin>>temp;  
    if(temp=='y'||temp=='Y')  
        sta=1;  
    else sta=3;  
}  
void result(int in)  
{  
    if(in>var)  
    {cout<<"��µ�̫���ˣ�\n";sta--;}  
    else if(in<var)  
    {cout<<"��µ�̫С�ˣ�\n";sta--;}  
    else   
    {cout<<"�������Ӯ�ˣ�"<<endl;sta-=11;}  
}  
void init()  
{  
    srand((int)time(0));  
    var=rand()%1000;  
    sta=9; 
}  
void game()  
{  
    init();  
    cout<<"������һ��������1000����Ȼ��:\n";  
    for(;sta>=1;)  
    {  
        int in;  
        cin>>in;  
        result(in);  
    }  
    if(sta==0)end_game();  
    sta=0;  
}  
int main()  
{  
    for ( ; sta!=0; )  
    {  
        switch (sta)  
        {  
        case 1:game();  
        case 2:{conti();break;}  
        case 3:{cout<<"��ӭ�´�����!\n";sta=0;break;}  
        }  
    }  
    return 0;  
}  
