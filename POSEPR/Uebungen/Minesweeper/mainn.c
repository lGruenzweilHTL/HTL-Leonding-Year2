/* Minesweeper 1 */
#include <stdio.h>

#define zeilenMax 4
#define spaltenMax 5

int getMaxTries(int matrix[zeilenMax][spaltenMax]) {
    int tries = 0;
    for (int i = 0; i < zeilenMax; i++) {
        for (int j = 0; j < spaltenMax; j++) {
            if (matrix[i][j] == 0) tries++; // Count the number of safe spaces
        }
    }
    return tries;
}
int getNeighbourMines(int matrix[zeilenMax][spaltenMax], int zeile, int spalte) {
    int startZeile = zeile - 1;
    if (startZeile < 0) startZeile = 0;
    int endZeile = zeile + 1;
    if (endZeile >= zeilenMax) endZeile = zeilenMax - 1;

    int startSpalte = spalte - 1;
    if (startSpalte < 0) startSpalte = 0;
    int endSpalte = spalte + 1;
    if (endSpalte >= spaltenMax) endSpalte = spaltenMax - 1;

    int n;
    for (int z = startZeile; z <= endZeile; z++) {
        for (int sp = startSpalte; sp <= endSpalte; sp++) {
            if (matrix[z][sp] == 9) n++;
        }
    }
    return n;
}

/******************Ausgabe****************/

int Ausgabe (char matrixAusgabe [zeilenMax] [spaltenMax])
{
	int zeile=0;
	int spalte=0;

	printf("\n");

	for (zeile=0; zeile<zeilenMax;zeile++)
	{
		for (spalte=0; spalte<spaltenMax; spalte++)
		{
			printf(" %c", matrixAusgabe [zeile] [spalte]);
		}
		printf("\n");
	}
	printf("\n");

	return 0;
}


/******************Ausgabe Test int-Matrix ****************/
int Ausgabe_int (int matrixAusgabe [zeilenMax] [spaltenMax])
{
	int zeile=0;
	int spalte=0;

	printf("\n");

	for (zeile=0; zeile<zeilenMax;zeile++)
	{
		for (spalte=0; spalte<spaltenMax; spalte++)
		{
			printf(" %d", matrixAusgabe [zeile] [spalte]);
		}
		printf("\n");
	}
	printf("\n");

	return 0;
}




/***********Hauptprogramm********/
int main (void)
{
	int matrix [zeilenMax] [spaltenMax]=
		{{0,0,9,0,0},
		{0,0,9,9,0},
		{0,0,9,0,0},
		{0,9,9,9,0},
		};
	char matrixAusgabe [zeilenMax] [spaltenMax]=
		{{'.','.','.','.','.'},
		 {'.','.','.','.','.'},
		 {'.','.','.','.','.'},
		 {'.','.','.','.','.'},
		};

	int zeile=0;
	int spalte=0;
	int versuche=0;
	int ende=0;
	int z;
	int sp;
    int maxTries = getMaxTries(matrix);
    printf("%d\n", maxTries);

	while (ende == 0)
	{
		Ausgabe (matrixAusgabe);
		
		printf("Bitte Zeile eingeben: ");
		scanf("%d",&zeile);
		zeile=zeile-1;

		printf("Bitte Spalte eingeben: ");
		scanf("%d",&spalte);
		spalte=spalte-1;
        getchar();

        char flag = 'n';
        printf("Flag [y/n]? ");
        scanf("%c", &flag);
		getchar();

        if (flag == 'y') {
            matrixAusgabe[zeile][spalte] = 'x';
            continue;
        }

        if (matrix[zeile][spalte] == 9) {
            ende = 1;
        }
        else {
            matrixAusgabe[zeile][spalte] = getNeighbourMines(matrix, zeile, spalte) + '0';
        }

        if (++versuche >= maxTries) {
            printf("You won!");
            ende = 1;
        }
	}
	printf ("Sie hatten %d erfolgreiche Versuche. \n", versuche);
	getchar();
	
	return (0);
}