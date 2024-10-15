#include <stdio.h>

int generateBitMask(int bit)
{
  return 1 << (bit - 1);
}

int turnOn(int x, int bit)
{
  return x | generateBitMask(bit);
}
int flip(int x, int bit)
{
  return x ^ generateBitMask(bit);
}
int turnOff(int x, int bit)
{
  return x & (~generateBitMask(bit));
}
int getBitValue(int x, int bit)
{
  int mask = generateBitMask(bit);
  return (x & mask) == mask;
}
int printNum(unsigned char reg)
{
  int i;
  for (i = 0; i < 8; i++)
  {
    if (reg & 128)
      printf("1");
    else
      printf("0");
    reg = reg << 1;
  }
  printf("\n");
}

int main(int argc, char **argv)
{
  int bit;
  int value;
  unsigned char x;

  printf("Geben Sie X ein: ");
  scanf("%d", &value);
  x = (char)value;

  printNum(x);

  printf("Welches Bit wollen Sie einschalten? ");
  scanf("%d", &bit);

  x = turnOn(x, bit);
  printNum(x);

  printf("Welches Bit wollen Sie umschalten? ");
  scanf("%d", &bit);

  x = flip(x, bit);
  printNum(x);

  printf("Welches Bit wollen Sie ausschalten? ");
  scanf("%d", &bit);

  x = turnOff(x, bit);
  printNum(x);

  printf("Welches Bit wollen Sie abfragen? ");
  scanf("%d", &bit);

  printf("Der Wer von Bit %d ist %d\n", bit, getBitValue(x, bit));
}