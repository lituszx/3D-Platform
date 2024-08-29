﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    private List<Command> _commands = new List<Command>();
    private int _currentCommandIndex;
    public InputReader readers;
    public void ExecuteCommand(Command command)
    {
        _commands.Add(command);
        command.Execute();
        _currentCommandIndex = _commands.Count - 1;
    }
    public void Undo()
    {
        if (_currentCommandIndex < 0)
            return;
            _commands[_currentCommandIndex].Undo();
            _commands.RemoveAt(_currentCommandIndex);
            _currentCommandIndex--;
        if(_commands.Count <= 0)
        {
            readers.currentClones = 0;
        }
    }
    public void Redo()
    {
        _commands[_currentCommandIndex].Execute();
        _currentCommandIndex++;
    }
}
