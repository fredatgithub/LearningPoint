﻿using Caliburn.Micro;
using EditingPrimitiveCollection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditingPrimitiveCollection
{
    public class BootstrapperSC : BootstrapperBase
    {
        #region Private Variable
        private SimpleContainer _Container = new SimpleContainer();
        #endregion

        #region Constructor
        public BootstrapperSC()
        {
            Initialize();
        }
        #endregion

        #region Overrides
        protected override void Configure()
        {
            _Container.Instance<IWindowManager>(new WindowManager());
            _Container.Singleton<IEventAggregator, EventAggregator>();

            // Hook up your ViewModel and Contract here
            _Container.PerRequest<ShellViewModel, ShellViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return _Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _Container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        #endregion
    }
}
