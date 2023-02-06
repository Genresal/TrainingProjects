// See https://aka.ms/new-console-template for more information

int p0 = 1500000;
double percent = 0.25;
int aug = 1000;
int p = 2000000;

percent = percent / 100;
int i;
for (i = 0; p0 < p; i++)
{
    p0 += (int)(p0 * percent) + aug;
    Console.WriteLine(p0);
}

Console.WriteLine(i);
