#include <stdio.h>
#include <stdlib.h>

struct StudentData {
    int GradeD;
    int GradeM;
    int GradeE;
    char *Name;
};

char *calculateSuccess(struct StudentData *data) {
    int d = data->GradeD;
    int m = data->GradeM;
    int e = data->GradeE;

    float avg = (d + m + e) / 3.0f;

    if (d == 5 || m == 5 || e == 5) {
        return "Nicht bestanden!";
    }
    if (avg <= 1.5f) {
        return "Mit ausgezeichnetem Erfolg bestanden!";
    }
    if (avg <= 2.0f) {
        return "Mit gutem Erfolg bestanden";
    }
    return "Bestanden";
}

void output(struct StudentData *data) {
    printf("\nResults for %s\n---------\n\n", data->Name);

    printf("German: %d\n", data->GradeD);
    printf("Math: %d\n", data->GradeM);
    printf("English: %d\n", data->GradeE);

    printf("\n%s\n", calculateSuccess(data));
}

int main(void) {
    struct StudentData student;

    printf("Input name: ");
    scanf("%s", student.Name);

    printf("Input german grade [1-5]: ");
    scanf("%d", &student.GradeD);

    printf("Input math grade [1-5]: ");
    scanf("%d", &student.GradeM);

    printf("Input english grade [1-5]: ");
    scanf("%d", &student.GradeE);

    output(&student);
}
