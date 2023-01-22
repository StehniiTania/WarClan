using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WarClan
{
    /// <summary>
    /// Логика взаимодействия для Field1.xaml
    /// </summary>
    public partial class Field1 : Window
    {
        public int column { set; get; }
        public int row { set; get; }
        public Clan clan1 { set; get; }
        public Clan clan2 { set; get; }
        public Battle battle { set; get; }       
        
        public ArmySave armySave1 { set; get; }
        public ArmySave armySave2 { set; get; }

        //паттерн Bridge
        InterfaceWindow interfaceWindow = new InterfaceWindowTextPicture();

        //паттерн ChainOfResponsibility
        Receiver receiver = new Receiver();        
        WarriorsCommandHandler warriorsCommandHandler = new WarriorsCommandHandler();
        DragonsCommandHandler dragonsCommandHandler = new DragonsCommandHandler();
        RobotsCommandHandler robotsCommandHandler = new RobotsCommandHandler();

        //pattern Momento
        TestState testState { set; get; }
        Caretaker caretaker { set; get; }


        public Field1(int scale)
        {
            InitializeComponent();

            //устанавливаем по умолчанию интерфейс - выводим текст + изображения
            interfaceWindow.Interface = new PictureTextInterface();
            interfaceWindow.DoInterface();

            column = scale;
            row = 6;

            if (scale == 21)
                Window2.Height = 600;
            if (scale == 15)
                Window2.Height = 700;
            if (scale == 9)
                Window2.Height = 900;

            //create clan-1
            clan1 = new Clan(1, column);
            //присваиваем координаты героям для старта
            for (int i = 0; i < clan1.army.Length; i++)            
                clan1.army[i].Сoordinate = i + column;   
            
            Thread.Sleep(100); //для корректного рандома

            //create clan-2
            clan2 = new Clan(2, column);
            //присваиваем координаты героям для старта
            for (int i = 0; i < clan2.army.Length; i++)
                clan2.army[i].Сoordinate = i + (row - 2) * column;

            battle = new Battle(clan1, clan2);

            testState = new TestState(clan1, clan2);
            caretaker = new Caretaker();

            //создание поля боя
            CreateField();         

        }        
        
        public void CreateField()
        {
            //create rows
            for (int i = 0; i < row; i++)
            {
                RowDefinition row_ = new RowDefinition();
                MainField.RowDefinitions.Add(row_);
            }
            
            //create columns
            for (int j = 0; j < column; j++)
            {                
                ColumnDefinition column_ = new ColumnDefinition();
                MainField.ColumnDefinitions.Add(column_);
            }

            //create grids
            for (int i = 0; i < row*column; i++)
            {
                Grid grid = new Grid();                                
                grid.Name = "Grid" + i;
                MainField.Children.Add(grid);
                Grid.SetRow(grid, i / column);
                Grid.SetColumn(grid, i % column);                
            }


            //filling grids with heroes
            if (interfaceWindow.DoInterface() == 3)
            {
                foreach (Grid grid1 in MainField.Children)
                    FillGridPictureText(grid1);
            }
            if (interfaceWindow.DoInterface() == 2)
            {
                foreach (Grid grid1 in MainField.Children)
                    FillGridPicture(grid1);
            }
            if (interfaceWindow.DoInterface() == 1)
            {
                foreach (Grid grid1 in MainField.Children)
                    FillGridText(grid1);
            }
        }

        //размещение героя в заданной точке (если он там есть)
        //интерфейс - изображения
        public void FillGridPicture(Grid grid)
        {
            int nomGrid = int.Parse(grid.Name.Substring(4));            

            for (int i = 0; i < clan1.army.Length; i++)
            {
                if (clan1.army[i].Сoordinate == nomGrid)
                {
                    //размещение картинки в соответствии с координатами
                    ShowHeroPicture(grid, clan1.army[i]);                    
                }
            }

            for (int i = 0; i < clan2.army.Length; i++)
            {
                if (clan2.army[i].Сoordinate == nomGrid)
                {
                    //размещение картинки в соответствии с координатами
                    ShowHeroPicture(grid, clan2.army[i]);
                }
            }
        }

        //размещение героя в заданной точке (если он там есть)
        //интерфейс - текст
        public void FillGridText(Grid grid)
        {
            int nomGrid = int.Parse(grid.Name.Substring(4));           

            for (int i = 0; i < clan1.army.Length; i++)
            {
                if (clan1.army[i].Сoordinate == nomGrid)
                {
                    //размещение текста в соответствии с координатами
                    ShowHeroText(grid, clan1.army[i]);                    
                }
            }

            for (int i = 0; i < clan2.army.Length; i++)
            {
                if (clan2.army[i].Сoordinate == nomGrid)
                {
                    //размещение текста в соответствии с координатами
                    ShowHeroText(grid, clan2.army[i]);
                }
            }
        }

        //размещение героя в заданной точке (если он там есть)
        //интерфейс - изображения + текст
        public void FillGridPictureText(Grid grid)
        {
            int nomGrid = int.Parse(grid.Name.Substring(4));
            Grid gridText = new Grid();
            
            for (int i = 0; i < clan1.army.Length; i++)
            {
                if (clan1.army[i].Сoordinate == nomGrid)
                {
                    //размещение картинки в соответствии с координатами
                    ShowHeroPicture(grid, clan1.army[i]);

                    //поиски грида для размещения текстового блока
                    foreach (Grid it in MainField.Children)
                    {
                        if (it.Name == "Grid" + (nomGrid - column))
                        {
                            gridText = it;
                        }
                    }

                    //размещение текстового блока выше картинки
                    ShowHeroText(gridText, clan1.army[i]);
                }
            }

            for (int i = 0; i < clan2.army.Length; i++)
            {
                if (clan2.army[i].Сoordinate == nomGrid)
                {
                    //размещение картинки в соответствии с координатами
                    ShowHeroPicture(grid, clan2.army[i]);

                    //поиски грида для размещения текстового блока
                    foreach (Grid it in MainField.Children)
                    {                        
                        if (it.Name == "Grid" + (nomGrid + column))
                        {
                            gridText = it;
                        }
                    }

                    //размещение текстового блока ниже картинки
                    ShowHeroText(gridText, clan2.army[i]);
                }
            }
        }

        //размещение изображения героя в определенном гриде
        public void ShowHeroPicture(Grid grid, Hero hero)
        {
            string path;
            path = hero.Picture;
            Uri uri = new Uri(path, UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
            if (hero.NumberLives >= 3) img.Opacity = 1;
            if (hero.NumberLives > 1 && hero.NumberLives < 3) img.Opacity = 0.6;
            if (hero.NumberLives == 0) img.Opacity = 0.4;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            grid.Children.Add(img);
        }

        //размещение текстового блока героя в определенном гриде
        public void ShowHeroText(Grid grid, Hero hero)
        {
            string lives = "", status = "";
            //string isShoot = "";
            //if (hero.isShoot) isShoot = "battle + ";
            if (hero.State is HeroStateWounded) status = "\u2690";
            if (hero.State is HeroStateDied) status = "\u2691";          

            for (int i = 0; i < hero.NumberLives; i++)
                lives += "\u2605";

            TextBlock textBlock = new TextBlock
            {
                Text = hero.Name + "\n " +
                hero.Weapon + "\n " + lives + "\n  "
                 + status,
                FontSize = 9 * 21/column,
                Width = ((Width - 115) / column) - 10,
                Height = ((Height - 140) / row) - 10,
                Background = hero.ColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center                
            };
            grid.Children.Add(textBlock);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            RestartMainGrid();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Interface_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "IText")
            {
                interfaceWindow.Interface = new TextInterface();
                interfaceWindow.DoInterface();
                RestartMainGrid();
            }
            if ((sender as Button).Name == "IPicture")
            {
                interfaceWindow.Interface = new PictureInterface();
                interfaceWindow.DoInterface();
                RestartMainGrid();
            }
            if ((sender as Button).Name == "ITextPicture")
            {
                interfaceWindow.Interface = new PictureTextInterface();
                interfaceWindow.DoInterface();
                RestartMainGrid();
            }
        }
        
        private void RestartMainGrid()
        {
            MainField.Children.Clear();
            MainField.RowDefinitions.Clear();
            MainField.ColumnDefinitions.Clear();
            CreateField();
        }

        //отработка нажатия клавиш команд обоих кланов:
        //"Wa1_Forward", "Wa1_Back", "Wa1_Attack", "Dr1_Forward", "Dr1_Back",
        //"Dr1_Attack", "Ro1_Forward", "Ro1_Back", "Ro1_Attack"
        //"Wa2_Forward", "Wa2_Back", "Wa2_Attack", "Dr2_Forward", "Dr2_Back",
        //"Dr2_Attack", "Ro2_Forward", "Ro2_Back", "Ro2_Attack"
        private void CommandClan_Click(object sender, RoutedEventArgs e)
        {
            //pattern "ChainOfResponsibility"

            //обрезаем имя кнопки так, чтобы получить название команды
            string nameCommand = (sender as Button).Name.Substring(4);

            if ((sender as Button).Name.Substring(2).StartsWith("1"))
            {
                warriorsCommandHandler = new WarriorsCommandHandler(clan1, nameCommand);
                dragonsCommandHandler = new DragonsCommandHandler(clan1, nameCommand);
                robotsCommandHandler = new RobotsCommandHandler(clan1, nameCommand);
            }
            else
            {
                warriorsCommandHandler = new WarriorsCommandHandler(clan2, nameCommand);
                dragonsCommandHandler = new DragonsCommandHandler(clan2, nameCommand);
                robotsCommandHandler = new RobotsCommandHandler(clan2, nameCommand);
            }

            warriorsCommandHandler.Successor = dragonsCommandHandler;
            dragonsCommandHandler.Successor = robotsCommandHandler;

            if ((sender as Button).Name.StartsWith("W"))
                receiver = new Receiver(true, false, false);
            if ((sender as Button).Name.StartsWith("D"))
                receiver = new Receiver(false, true, false);
            if ((sender as Button).Name.StartsWith("R"))
                receiver = new Receiver(false, false, true);

            warriorsCommandHandler.Handle(receiver);

            if (nameCommand == "Attack")
                battle.StartBattle();

            RestartMainGrid();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            //armySave1  = new ArmySave(clan1.army);
            //armySave2 = new ArmySave(clan2.army);
            caretaker.SaveState(testState);

            MessageBox.Show("Game saved");
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            //armySave1.ArmyRestore(clan1.army);
            //armySave2.ArmyRestore(clan2.army);

            caretaker.RestoreState(testState);                

            RestartMainGrid();
            MessageBox.Show("Game restored");
        }
    }
}
