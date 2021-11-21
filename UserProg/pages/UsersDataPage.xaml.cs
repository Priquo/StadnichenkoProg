﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UserProg.pages
{
    public partial class UsersDataPage : Page
    {
        List<users> users;
        List<users> lu;
        public UsersDataPage()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            listbUsersList.ItemsSource = users;            
            cbGender.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            cbGender.SelectedValuePath = "id";
            cbGender.DisplayMemberPath = "gender";
            lu = users;
        }

        private void listb_traits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox list_u = (ListBox)sender;
            int id = Convert.ToInt32(list_u.Uid);
            list_u.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == id).ToList();
            list_u.DisplayMemberPath = "traits.trait";
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int OT = Convert.ToInt32(txtOT.Text) - 1;
                int DO = Convert.ToInt32(txtDO.Text);
                List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
                listbUsersList.ItemsSource = lu1;
            }
            catch
            {
                MessageBox.Show("Введите числа в обе графы!");
            }
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            listbUsersList.ItemsSource = users;
            txtOT.Text = "";
            txtDO.Text = "";
            txtbSearchName.Text = "";
            cbGender.SelectedItem = null;
        }

        private void buttBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
        private void Filters(object sender, RoutedEventArgs e)
        {
            try
            {
                //фильтр по полу
                if (cbGender.SelectedValue != null)
                {
                    lu = lu.Where(x => x.gender == (int)cbGender.SelectedValue).ToList();
                }
                //поиск по имени
                if (txtbSearchName.Text != "")
                {
                    lu = lu.Where(x => x.name.Contains(txtbSearchName.Text)).ToList();
                }
                listbUsersList.ItemsSource = lu;
            }
            catch
            {
                MessageBox.Show("Да и черт с ним");
            }
        }

        private void txtbShowPages_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
