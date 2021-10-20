using Itibsoft.ConsoleDeveloper.Core;
using Itibsoft.ConsoleDeveloper.Utils;
using UnityEngine;

namespace Itibsoft.ConsoleDeveloper.Console
{
	public class ConsoleDeveloper : MonoBehaviour
	{
		[SerializeField] private InputHandler _inputHandler;
		[SerializeField] private GameObject _consoleObject;
		[SerializeField] private Logger _logger;
		[SerializeField] private Buffer _buffer;
		[SerializeField] private Input _input;

		private void Start()
		{
			_inputHandler.OnKeyDown += OnKeyHandler;
		}

		private void OnKeyHandler(KeyCode key)
		{
			switch(key)
			{
				case KeyCode.BackQuote:
					_consoleObject.SetActive(!_consoleObject.activeInHierarchy);
					break;
				case KeyCode.Return:
					Send();
					break;
			}
		}

		public void Send()
		{
			ICommand command = CommandsList.GetCommand(_input.GetInputText());

			if (command != null) ExecuteCommand(command);
			else Logger.Instance.AddLog(Tools.SetColorText($"Error: ", TypeColor.Red) + _input.GetInputText() + " - " + Tools.SetColorText("This command is not recognized", TypeColor.Yellow));

			_input.ClearInputField();
		}

		private void ExecuteCommand(ICommand command)
		{
			command.Execute();
		}
	}
}
