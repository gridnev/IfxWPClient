using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Autofac;
using Common.Interfaces;
using BLL;
using Services;

namespace IfxWPClient.Infrastructure
{
    public class ClientModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewsSource>().AsSelf().SingleInstance();
            builder.RegisterType<IFXWebService>().As<IIFXWebService>().SingleInstance();
            builder.RegisterType<NewsSource>().As<INewsDataSource>().SingleInstance();
        }
    }
}
