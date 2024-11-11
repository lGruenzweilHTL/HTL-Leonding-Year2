#include <stdio.h>

void executeCommand(char *args[])
{
	// As parent process, fork the process, then return method
	if (fork() == 0)
	{
		// As child process, execute command
		execvp(args[0], args);
	}
}

void main(int argc, char **argv)
{
	char *ls[] = {"ls", NULL};
	char *df[] = {"df", NULL};
	char *pf[] = {"ps", NULL};
	char *pwd[] = {"pwd", NULL};

	printf("Executing command: ls\n");
	executeCommand(ls);
	printf("Executing command: df\n");
	executeCommand(df);
	printf("Executing command: pf\n");
	executeCommand(pf);
	printf("Executing command: pwd\n");
	executeCommand(pwd);

	int status;
	waitpid(-1, &status, 0); // Wait for all created processes to be finished

	printf("Done");
	return status;
}