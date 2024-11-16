#include <stdio.h>
#include <locale.h>
#include <fstream>
#include <string>
#include <iostream>
using namespace std;

struct list
{
   int elem;
   list *next;
   list(int _elem = 0, list *_next = NULL) : elem(_elem), next(_next) {}
} *l = new list;

bool inputFile(list *t)
{
   bool IsOk = true;
   FILE *f = NULL;
   
   if ((f = fopen("in.txt", "r")) != NULL)
   {
      fseek(f, 0, SEEK_END);
      if (!ftell(f))
      {
         printf("Файл in.txt пустой.\n");
         IsOk = false;
      }
      else
      {
         fseek(f, 0, SEEK_SET);
         for (list *p = t->next = new list; !feof(f) && IsOk; p = p->next = new list)
             IsOk = fscanf(f, "%d", &p->elem);

         if (!IsOk)
            printf("Файл содержит недопустимые данные.\n");
      }
   }
   else
   {
      printf("Файл in.txt не найден.\n");
      IsOk = false;
   }
   return IsOk;
}

bool inputConsole(list *t) {
   scanf("%*c");
   printf("Введите последовательность чисел: ");
   bool IsOk = true;

   for (list *p = t->next = new list; IsOk; ) {
      IsOk = scanf("%d", &p->elem);
      p = p->next = new list;

      if (getchar() == '\n') 
         break;
   }

   if (!IsOk)
      printf("Введены некорректные данные.\n");

   return IsOk;
}

void reverseToConsole(list *h)
{
   list *p = h->next;
   if (p->next)
   {
      reverseToConsole(p);
      printf("%d ", p->elem);
   }
}

    void reverseToFile(list *h, FILE *f)
{
   list *p = h->next;
   if (p->next)
   {
      reverseToFile(p, f);
      fprintf(f, "%d ", p->elem);
   }
}

void printManual()
{
   ifstream file("manual.txt");
   string line;

   while (getline(file, line))
      cout << line << endl;

   file.close();
}

int main()
{
   setlocale(0, "");
   int inputMethod = 0;

   do
   {
      printf("Выберите способ ввода последовательности чисел:\n");
      printf("1. Данные из файла \"in.txt\".\n");
      printf("2. Ввести вручную.\n");
      printf("3. Справка.\n");
      printf("Ваш выбор: ");

      if (scanf("%d", &inputMethod) != 1 || (inputMethod != 1 && inputMethod != 2 && inputMethod != 3))
      {
         printf("Некорректный выбор способа ввода данных.\n\n");
         while (getchar() != '\n');
      }
   } while (inputMethod != 1 && inputMethod != 2 && inputMethod != 3);

   bool isCorrect;
   
   if (inputMethod == 1)
      isCorrect = inputFile(l);
   else if (inputMethod == 2)
      isCorrect = inputConsole(l);
   else
   {
      printf("\n");
      printManual();
      printf("\n");
      return 0;
   }   

   if (isCorrect)
   {
      int outputMethod;

      do
      {
         printf("Выберите способ вывода результата:\n");
         printf("1. Вывести в файл \"out.txt\".\n");
         printf("2. Вывести в консоль.\n");
         printf("Ваш выбор: ");

         if (scanf("%d", &outputMethod) != 1 || (outputMethod != 1 && outputMethod != 2))
         {
            printf("Некорректный выбор способа ввода данных.\n\n");
            while (getchar() != '\n');
         }
      } while (outputMethod != 1 && outputMethod != 2);

      if (outputMethod == 1) 
      {
         FILE *f = NULL;
         f = fopen("out.txt", "w");

         reverseToFile(l, f);
         fclose(f);
      }
      else
      {
         printf("Последовательность в обратном порядке: ");
         reverseToConsole(l);
         printf("\n");
      }
   }
   return 0;
}