using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAndAnswer
{
	class QuestionAndAnswers
	{
		public bool IsAnswered { get; set; }
		public string Question { get; set; }

		public List<string> Answers = new List<string>();
		public string CorrectAnswer { get; set; }

		public QuestionAndAnswers(string question, string answer1, string answer2)
		{
			IsAnswered = false;
			Question = question;
			Answers.Add(answer1);
			Answers.Add(answer2);
			CorrectAnswer = answer1;
		}

		public QuestionAndAnswers(string question, string answer1, string answer2, string answer3)
		{
			IsAnswered = false;
			Question = question;
			Answers.Add(answer1);
			Answers.Add(answer2);
			Answers.Add(answer3);
			CorrectAnswer = answer1;
		}

		public QuestionAndAnswers(string question, string answer1, string answer2, string answer3,
			string answer4)
		{
			IsAnswered = false;
			Question = question;
			Answers.Add(answer1);
			Answers.Add(answer2);
			Answers.Add(answer3);
			Answers.Add(answer4);
			CorrectAnswer = answer1;
		}

		public bool CheckCorrectAnswer(string answer)
		{
			if (answer == CorrectAnswer) return true;
			return false;
		}
	}
}
