// TODO: Execute ls, df, ps, pwd in parallel. Then write Done to the stdout
#include <stdio.h>
void executeCommand(char *args[]){
	if (fork() != 0){
		// Parent code
		// Do nothing
	}
	else
	{
	  printf("Got here!\n");
		// Child code
		execvp(args[0], args);
	}
}

void main(int argc, char **argv){
	char *lsArgs[] = {"ls", NULL};
	char *dfArgs[] = {"df", NULL};
	char *pfArgs[] = {"ps", NULL};
	char *pwdArgs[] = {"pwd", NULL};

  printf("Executing command: ls\n");
	executeCommand(lsArgs);
	printf("Executing command: df\n");
	executeCommand(dfArgs);
	printf("Executing command: pf\n");
	executeCommand(pfArgs);
	printf("Executing command: pwd\n");
	executeCommand(pwdArgs);

	int status;
	waitpid(-1, &status, 0); // Wait for all created processes to be finished

	printf("Done");
	return 0;
}