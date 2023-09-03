using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1oobjmain
{
    public class CommandHistory
    {
        private readonly Stack<ICommand> _history = new Stack<ICommand>();
        private readonly Stack<ICommand> _undoHistory = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            command.Execute();
            _history.Push(command);
        }
        public void Add(ICommand command)
        {
            _history.Push(command);
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.UnExecute();
                _history.Push(command);
                //_undoHistory.Push(command);
            }
        }

        public void Redo()
        {
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.Execute();
                _history.Push(command);
                //_history.Push(command);
            }
        }

        public void PrintHistory()
        {
            foreach (var command in _history)
            {
                Console.WriteLine(command.ToString());
            }
        }
    }
}
