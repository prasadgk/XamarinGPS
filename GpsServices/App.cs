using System;
using System.Collections.Generic;
using System.Text;
using GpsServices.Services;
using GpsServices.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GpsServices
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterType<ICalculationService, CalculationService>();

            RegisterAppStart<TipViewModel>();
        }
    }
}