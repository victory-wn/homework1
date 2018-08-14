# 汪能的demo
## 一个猜数小游戏
**Getting Started:** 需要用到c++编程软件

**Running the tests:** 打开xxx.cpp文件，复制源代码，在c++编程软件内运行源代码即可

源代码：
```c++
#include<iostream>   
#include<time.h>  
using namespace std;  
int sta=1;  
int var=0;  
void end_game()  
{  
    {cout<<"你输了！"<<"告诉你，正确答案是"<<var<<endl;}  
}  
void conti()  
{  
    cout<<"再来一局? 是(输入y) or 否(输入其它):\n";  
    char temp;  
    cin>>temp;  
    if(temp=='y'||temp=='Y')  
        sta=1;  
    else sta=3;  
}  
void result(int in)  
{  
    if(in>var)  
    {cout<<"你猜的太大了！\n";sta--;}  
    else if(in<var)  
    {cout<<"你猜的太小了！\n";sta--;}  
    else   
    {cout<<"真棒！你赢了！"<<endl;sta-=11;}  
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
    cout<<"请输入一个不大于1000的自然数:\n";  
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
        case 3:{cout<<"欢迎下次再玩!\n";sta=0;break;}  
        }  
    }  
    return 0;  
}  
```

**Usage:** 在c++编程软件里面运行源代码，会随机生成一个不大于1000的自然数，并提示你输入一个不大于1000的自然数，随后告诉你所猜数字的大小，你有9次猜数的机会，猜出随机生成的数则取得了胜利。

**Contributing:** 汪能

**Authors:** 汪能

## 致谢！