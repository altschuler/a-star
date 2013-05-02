REFS= -r:System.Windows.Forms -r:System.Drawing
FILES = $(wildcard *.cs)
build:
	dmcs -debug $(REFS) $(FILES)

run: 
	mono --debug ActionAbstract.exe

bar: build run
