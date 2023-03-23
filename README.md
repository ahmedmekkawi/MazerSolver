# Mazer Solver
# A simple console app written in c#
# Upon running the app, you will need to provide the input full path of the maze text file, 
# as well as the path where you want the result to be generated.

# The following creterias has been followed:

1.	Be able to read in a text file that would contain the following:
    a.	‘X’ = Wall
    b.	‘S’ = Start Point
    c.	‘E’ = End Point
2.	Write the logic to solve the maze
    a.	Width and height will be between (3 and 255 spaces)
    b.	Width and height may not be the same
    c.	May have multiple paths – only choose 1
    d.	Will always be surrounded by ‘X’ (Frame)
    e.	Will always have 1 ‘S’ and 1 ‘E’ in every maze.
3.	Output maze to text file showing the path used using ‘.’
4.	Does not need to be shortest path, however keep it in mind on how it would be done.

Example Input:
XXXXXXXXXX
XS       X
XXXXXXX  X
XE    X  X
XXXXX X  X
X        X
XXXXXXXXXX

Example Output:
XXXXXXXXXX
XS...... X
XXXXXXX. X
XE....X. X
XXXXX.X. X
X    ... X
XXXXXXXXXX
