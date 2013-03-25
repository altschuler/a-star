REFS= -r:System.Windows.Forms -r:System.Drawing

build:
	mcs -debug $(REFS) MainWindow.cs SearchResult.cs KnowledgeBase.cs PriorityQueue.cs Painter.cs AStarSearcher.cs Action.cs State.cs Node.cs 

run: 
	mono --debug MainWindow.exe

bar: build run