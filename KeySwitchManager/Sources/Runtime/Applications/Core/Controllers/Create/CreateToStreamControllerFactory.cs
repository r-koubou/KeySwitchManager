﻿using System;
using System.IO;

using KeySwitchManager.Applications.Core.Controllers.Export;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateToStreamControllerFactory : ICreateToStreamControllerFactory
    {
        public IController Create( Stream targetStream, ExportSupportedFormat format, ILogTextView logTextView )
        {
            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory = new ExportLeaveOpenedStreamContentWriterFactory( targetStream );
            IExportStrategy strategy;

            switch( format )
            {
                case ExportSupportedFormat.Yaml:
                    contentFactory       = new YamlExportContentFactory();
                    strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                case ExportSupportedFormat.Xlsx:
                    contentFactory       = new YamlExportContentFactory();
                    strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( format ), format, null );
            }

            return new CreateFileController( strategy, new ICreatePresenter.Null() );
        }
    }
}
