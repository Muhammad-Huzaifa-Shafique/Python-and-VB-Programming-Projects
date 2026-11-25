# Python and VB Programming Projects

Small practice exercises from my graduate course (GRT713). I keep them together here, apart from my main projects, which are listed on [my profile](https://github.com/Muhammad-Huzaifa-Shafique).

The neural network assignments (a NumPy forward pass and backpropagation, ten Keras experiments on California housing prices, and ten CNN variations on CIFAR-10) are in their own repository: [Neural-Networks-From-Scratch-to-CNNs](https://github.com/Muhammad-Huzaifa-Shafique/Neural-Networks-From-Scratch-to-CNNs), with an [interactive walkthrough](https://muhammad-huzaifa-shafique.github.io/deep-learning.html).

This repository has source files only: no data, no build folders and no virtual environments. For the Visual Basic apps it has just the form code (`Form1.vb`), not the full Visual Studio projects.

| File | Language | What it does |
|---|---|---|
| `Car.py` and `test_Car.py` | Python | A `Car` class that tracks speed, distance and average speed, with a small console loop to drive it, plus 10 unit tests. Run them with `python -m unittest`. |
| `LuckySeven_Form1.vb` | Visual Basic | A lucky-number game. SPIN draws three random numbers from 0 to 19 and shows a picture if any of them is 10. |
| `NameFormatter_Form1.vb` | Visual Basic | Loads a text file of "First Last, Contribution" lines, rewrites names as "Last First", removes duplicates, sorts by name or by contribution, and saves the list. |
| `ExcelToCsv_Form1.vb` | Visual Basic | Loads the first sheet of an Excel file into a grid and saves it as a CSV file. |
| `TextCleaner_Form1.vb` | Visual Basic | Pulls the text between `?Alice*` and `\Wonderland\` markers out of a file, keeps only allowed characters, fixes dashes and run-together words, and saves the cleaned text. |

Two of the Visual Basic files begin with a note linking the AI conversation I used (ChatGPT in one, GitHub Copilot in the other). I left those notes as they were.

## Author

Made by [Muhammad Huzaifa Shafique](https://github.com/Muhammad-Huzaifa-Shafique).
