﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LotR.Effects;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;

using LotR.Client.Extensions;
using LotR.Client.ViewModels;

namespace LotR.Client.Controls
{
    /// <summary>
    /// Interaction logic for ChoiceControl.xaml
    /// </summary>
    public partial class ChoiceControl : UserControl
    {
        public ChoiceControl()
        {
            InitializeComponent();
        }

        private IGame game;
        private IChoice choice;
        private ICost cost;
        private IPayment payment;
        private ChoiceItemViewModel choiceViewModel;
        private bool isValid;
        private bool isCancelled;

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (choice == null)
                return;

            if (!choice.IsValid(game))
            {
                MessageBox.Show("Please choose an answer and then try submitting again", "This choice is not valid");
                statusText.Text = "This choice is not valid";
                return;
            }

            if (cost != null)
            {

            }

            isValid = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (choice == null)
            {
                isCancelled = true;
                return;
            }

            if (!choice.IsOptional)
            {
                MessageBox.Show("You must make this choice before the game can continue", "This Choice Is Not Optional");
                return;
            }

            isCancelled = true;
        }

        private void CheckBoxChanged(object sender)
        {
            var element = sender as UIElement;
            if (element == null)
                return;

            var item = element.FindContainerItem<TreeViewItem>();
            if (item == null)
                return;

            var viewModel = item.DataContext as ChoiceItemViewModel;
            if (viewModel != null)
            {
                if (viewModel.Parent != null && viewModel.Parent.MaximumChosenAnswers > 0)
                {
                    var numberChosen = viewModel.Parent.Children.Where(x => x.IsChosen).Count();
                    while (numberChosen > viewModel.Parent.MinimumChosenAnswers)
                    {
                        numberChosen--;
                        var first = viewModel.Parent.Children.Where(x => x.IsChosen && x.ItemId != viewModel.ItemId).FirstOrDefault();
                        if (first == null)
                            continue;

                        first.IsChosen = false;
                    }
                }

            }
        }

        private void chosenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxChanged(sender);
        }

        private void chosenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxChanged(sender);
        }

        public bool IsValid
        {
            get { return isValid; }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
        }

        public IPayment Payment
        {
            get { return payment; }
        }

        public void Initialize(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;
        }

        public void Load(IChoice choice)
        {
            if (choice == null)
                throw new ArgumentNullException("choice");

            this.choice = choice;
            this.cost = null;
            this.payment = null;

            this.choiceViewModel = new ChoiceItemViewModel(this.Dispatcher, choice);
            this.isValid = false;
            this.isCancelled = false;
            this.statusText.Text = string.Empty;

            choiceContainer.DataContext = choiceViewModel;
            choiceChildrenContainer.ItemsSource = choiceViewModel.Children;
        }
    }
}
