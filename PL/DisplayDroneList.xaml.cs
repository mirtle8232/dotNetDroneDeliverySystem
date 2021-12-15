﻿using IBL.BO;
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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        IBL.IBL Bl;
        int droneStatus = -1;
        int droneMaxWeight = -1;
        public DroneList(IBL.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            dronesDisplay.ItemsSource = Bl.ReturnDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
            List<ComboBoxItem> itemList = new List<ComboBoxItem>(); 
            
            for (int i = 0; i < 3; i++) {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (EnumBL.DroneStatusesBL)(i);
                itemList.Add(newItem);
            }
            OrderByStatus.ItemsSource = itemList;
            itemList = new List<ComboBoxItem>();
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (EnumBL.WeightCategoriesBL)(i);
                itemList.Add(newItem);
            }
            OrderByMaxWeight.ItemsSource = itemList;
        }
        
        private void comboBoxOByStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox senderCB = sender as ComboBox;
            droneStatus = senderCB.SelectedIndex;
            dronesDisplay.ItemsSource = Bl.ReturnDronesByStatusAndMaxW(droneStatus, droneMaxWeight);

        }
        private void comboBoxOByMaxW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderCB = sender as ComboBox;
            droneMaxWeight = senderCB.SelectedIndex;
            dronesDisplay.ItemsSource = Bl.ReturnDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
        }

        private void buttonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            OrderByStatus.Text = string.Empty;
            OrderByMaxWeight.Text = string.Empty;
            droneStatus = -1; droneMaxWeight = -1;
            dronesDisplay.ItemsSource = Bl.ReturnDronesByStatusAndMaxW(droneStatus, droneMaxWeight);
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IBL.BO.DroneBL drone = (sender as ListView).SelectedValue as IBL.BO.DroneBL;
            new DisplayDrone(Bl, drone).Show();
        }


        private void ButtonAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DisplayDrone(Bl).Show();
        }

        private void dronesDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new DisplayDrone(Bl).Show();
        }
    }
}




















































































/*            switch (senderCB.SelectedIndex)
            {
                case 0:
                    showEmptyDrones();
                    break;
                case 1:
                    showMaintenaceDrones();
                    break;
                case 2:
                    showShippingDrones();
                    break;
            }
        }
        public void showEmptyDrones()
        {
            MessageBox.Show("empty drone list");
        }
        public void showMaintenaceDrones()
        {
            MessageBox.Show("mainstain drone list");
        }
        public void showShippingDrones()
        {
            MessageBox.Show("shipping drone list");
        }*/