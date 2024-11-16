#include <cstdio>
#include <ctype.h>
#include <locale.h>

struct list
{
   char elem;
   list *next;

   list(char _elem = 0, list *_next = NULL) : elem(_elem), next(_next) {}
};
typedef list stack;

stack *push(char c = 0, stack *s = NULL)
{
   stack *p = new stack(c, s);
   return p;
}

stack *pop(stack *s = NULL)
{
   stack *p = s;
   s = s->next;
   delete p;
   return s;
}

stack *expressionCalculation (stack *s, char c)
{
   if (!s || !s->next)
   {
      printf("Данные введены некорректно.");
      return NULL;
   }
   else
   {
      char op2 = s->elem;
      s = pop(s);
      char op1 = s->elem;
      s = pop(s);

      switch (c)
      {
         case '-':
            s = push(op1 - op2, s); return s;
         case '+':
            s = push(op1 + op2, s); return s;
         case '*':
            s = push(op1 * op2, s); return s;
         default:
         {
            printf("Выражение содержит недопустимые символы.");
            return NULL;
         }
      }
   }
}

stack *inputConsole(stack *s = NULL)
{
   char c = 0;
   bool isPostfix = false;
   int operandsCount = 0;
   int operatorsCount = 0;

   printf("Введите постфиксное выражение: ");
   getchar();

   for (char c = getchar(); c != '\n' && c != EOF; c = getchar())
   {
      if (isdigit(c))
      {
         s = push(c - '0', s);
         
         isPostfix = true;
         operandsCount++;
      }
      else
      {
         if (isPostfix)
         {
            s = expressionCalculation(s, c);
            if (!s)
               return NULL;
            operatorsCount++;
         }
         else
         {
            printf("Введены некорректные данные.");
            return NULL;
         }
      }
   }

   if (operandsCount != operatorsCount + 1)
   {
      printf("Введены некорректные данные.\n");
      return NULL;
   }
   return s;
}

stack *inputFile(stack *s = NULL)
{
   FILE *f = NULL;
   bool isPostfix = false;
   int operandsCount = 0;
   int operatorsCount = 0;

   if ((f = fopen("in.txt", "r")))
   {
      char c = 0;
      for ( ; !feof(f) && (c = fgetc(f)) != EOF; )
      {
         if (isdigit(c))
         {
            s = push(c - '0', s);

            isPostfix = true;
            operandsCount++;
         }            
         else
         {
            if (isPostfix)
            {
               s = expressionCalculation(s, c);
               if (!s)
                  return NULL;
               operatorsCount++;
            }
            else
            {
               printf("Введены некорректные данные.");
               return NULL;
            }
         }
      }
      if (operandsCount != operatorsCount + 1)
      {
         printf("Введены некорректные данные.\n");
         return NULL;
      }
      return s;
   }
   else
   {
      printf("Возникла ошибка при открытии файла.");
      return NULL;
   }
   fclose(f);
   return s;
}

void outputFile(stack *s = NULL)
{
   FILE *f = NULL;

   if ((f = fopen("out.txt", "w")))
   {
      fprintf(f, "Значение выражения = %d", s->elem);
      fclose(f);
   }
}

void outputConsole(stack *s = NULL) { printf("Значение выражения = %d", s->elem); }

int main()
{
   setlocale(0, "");

   stack *s = NULL;
   int inputMethod = 0;

   do
   {
      printf("Выберите способ ввода постфиксного выражения: \n");
      printf("1. Данные из файла \"in.txt\". \n");
      printf("2. Ввести вручную. ");

      if (scanf("%d", &inputMethod) != 1 || (inputMethod != 1 && inputMethod != 2))
      {
         printf("Некорректный выбор способа ввода данных.\n\n");
         while (getchar() != '\n');
      }
   } while (inputMethod != 1 && inputMethod != 2);

   if (inputMethod == 1)
      s = inputFile(s);
   else if (inputMethod == 2)
      s = inputConsole(s);

   if (s)
   {
      int outputMethod = 0;

      do
      {
         printf("Выберите способ вывода результата вычисления: \n");
         printf("1. Вывести в файл \"out.txt\". \n");
         printf("2. Вывести в консоль. ");

         if (scanf("%d", &outputMethod) != 1 || (outputMethod != 1 && outputMethod != 2))
         {
            printf("Некорректный выбор способа вывода данных.\n\n");
            while (getchar() != '\n');
         }
      } while (outputMethod != 1 && outputMethod != 2);

      if (outputMethod == 1)
         outputFile(s);
      else if (outputMethod == 2) {
         outputConsole(s);
         printf("\n");
      }
   }   
}
