REFS= -r:System.Windows.Forms -r:System.Drawing

build:
	mcs $(REFS) MainWindow.cs KnowledgeBase.cs PriorityQueue.cs Painter.cs AStarSearcher.cs Action.cs State.cs Node.cs 

run: 
	mono MainWindow.exe

bar: build run