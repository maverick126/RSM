using System;
using System.Collections.Generic;
using System.Text;

namespace SQSAdmin
{
	interface IInputForm
	{

		void ViewNew();
		void ViewEdit();
		void ViewRead();
		void SaveForm();
		void ClearForm();

		
	}

	public enum InputFormMode
	{
		New = 0,
		Edit = 1,
		Read = 2,
		
	}


}
