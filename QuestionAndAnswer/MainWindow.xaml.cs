using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuestionAndAnswer
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		List<QuestionAndAnswers> listOfQuestionAndAnswers = new List<QuestionAndAnswers>();

		// list zawiera randomowe i niepowtarzające się liczby. Jest to potrzebne aby
		// pytania nie występowały ponownie
		List<int> randomQuestion = new List<int>();
		private int nextQuestion = 0; // jest odpowiedzialny za index w liście randomQuestion. JEst on globalny ponieawż będzie użyty w 2 metodach
		List<int> randomAnswers; // randomowe pytania
		private int nextAnswer = 0;
		private Dictionary<int, List<int>> dictionaryOfQAndA;

		private int score;

		public MainWindow()
		{
			InitializeComponent();
			InitializeData();
		}

		private void InitializeData()
		{
			listOfQuestionAndAnswers.Add(new QuestionAndAnswers("ile to jest 2 + 2 ?", "4", "3", "1", "5"));
			listOfQuestionAndAnswers.Add(new QuestionAndAnswers("ile to jest 1 + 1 ?", "2", "1"));
			listOfQuestionAndAnswers.Add(new QuestionAndAnswers("ile to jest 5 + 5 ?", "10", "20", "30"));
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			Random gen = new Random();
			int number;

			// w słowniku znajdują się pytania i odpowiedzi w randomowej kolejności
			dictionaryOfQAndA = new Dictionary<int, List<int>>();

			// pętla generuje randomowe miejsca na pytania
			for (int i = 0; i < listOfQuestionAndAnswers.Count; i++)
			{
				// wstaw do listy liczbę, jeżeli liczba znajduje się już w liście to szukaj innej randomowej
				do number = gen.Next(0, listOfQuestionAndAnswers.Count);
				while (randomQuestion.Contains(number));

				randomQuestion.Add(number);
				randomAnswers = new List<int>();

				// pętla generuje randomowe miejsca na odpowiedzi
				for (int j = 0; j < listOfQuestionAndAnswers[randomQuestion[i]].Answers.Count(); j++)
				{
					do number = gen.Next(0, listOfQuestionAndAnswers[randomQuestion[i]].Answers.Count());
					while (randomAnswers.Contains(number));

					randomAnswers.Add(number);
				}

				dictionaryOfQAndA.Add(randomQuestion[i], randomAnswers);
			}

			MakeQuestion();
		}

		private void QuestionButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (nextQuestion < listOfQuestionAndAnswers.Count)
			{
				CheckAnswer();
				MakeQuestion();
			}
		}

		private void MakeQuestion()
		{
			if (nextQuestion < listOfQuestionAndAnswers.Count)
			{
				QuestionTxtBlock.Text = listOfQuestionAndAnswers[dictionaryOfQAndA.ElementAt(nextQuestion).Key].Question;
				int countOfAnswers = listOfQuestionAndAnswers[randomQuestion[nextAnswer]].Answers.Count();

				// pętla tworzy radioButtony i dodaje do nich odpowiedzi
				for (int i = 0; i < countOfAnswers; i++)
				{
					RadioButton radioButtonAnswer = new RadioButton();
					radioButtonAnswer.Content =
						listOfQuestionAndAnswers[dictionaryOfQAndA.ElementAt(nextAnswer).Key].Answers[
							dictionaryOfQAndA.ElementAt(nextAnswer).Value[i]];
					StackPanelAnswers.Children.Add(radioButtonAnswer);
				}
			}
			else
			{
				QuestionButton.IsEnabled = false;

				TextBlock viewScore = new TextBlock()
				{
					Text = "Your Score: " + score + "/" + listOfQuestionAndAnswers.Count,
					FontSize = 20,
					FontWeight = FontWeights.Bold
				};

				StackPanelAnswers.Children.Add(viewScore);
			}
			

		}

		private void CheckAnswer()
		{
			foreach (RadioButton item in StackPanelAnswers.Children)
			{
				if (item.IsChecked.Value)
				{
					if (item.Content == listOfQuestionAndAnswers[dictionaryOfQAndA.ElementAt(nextAnswer).Key].CorrectAnswer)
					{
						score++;
					}
				}
				item.IsEnabled = false;
				item.Visibility = Visibility.Collapsed;
			}

			nextAnswer++;
			nextQuestion++;
		}

	}
}
