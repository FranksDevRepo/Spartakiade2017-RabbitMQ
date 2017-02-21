﻿using System;
using FP.Spartakiade2017.PicFlow.Contracts.FileHandler;
using FP.Spartakiade2017.PicFlow.Contracts.Messages;
using FP.Spartakiade2017.PicFlow.WebApp.Models;
using Nancy;
using Nancy.ModelBinding;

namespace FP.Spartakiade2017.PicFlow.WebApp.Modules
{
    public class ImageModule : NancyModule
    {

        public ImageModule(IFileHandler fileUploadHandler, MessageRepository messageRepo, ImageRepository imageRepo, AuthenticationRepository authRepo)
        {

            Get("/imagerequest", args =>
            {
                var identity = this.Context.CurrentUser;
                if (identity == null)
                {
                    return View["Login"];
                }
                else
                {
                    return View["ImageRequest"];
                }
            });

            Post("/imagerequest", async args =>
            {
                var request = this.Bind<ImageRequest>();

                var uploadResult = await fileUploadHandler.HandleUpload(request.File.Name, request.File.Value);
                var identity = this.Context.CurrentUser;
                if (identity == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var userId = Guid.Parse(identity.Identity.Name);
                var user = authRepo.GetAuthUserByUserId(userId);
                

                if (request.PostImage)
                {
                    var procJob = new ImageProcessingJob
                    {
                        Id = uploadResult.Identifier,
                        Overlay = request.EventOverlay,
                        Resolution = 2
                    };
                    var uploadJob = new ImageUploadJob
                    {
                        User = user.User,
                        Message = request.Message
                    };
                    procJob.Successors.Add(uploadJob);
                    await messageRepo.SendImageProcessingJob(procJob);
                }

                foreach (var resolution in request.Resolutions)
                {
                    var procJob = new ImageProcessingJob
                    {
                        Id = uploadResult.Identifier,
                        Overlay = request.EventOverlay,
                        Resolution = resolution
                    };
                    var saveJob = new ImageSaveJob
                    {
                        UserId = userId,
                        Message = request.Message,
                        SourceId = uploadResult.Identifier,
                        Resolution = resolution
                    };
                    procJob.Successors.Add(saveJob);
                    await messageRepo.SendImageProcessingJob(procJob);
                }
                var model = new Home { Message = $"Auftrag mit Bild {request.File?.Name} wurde gestartet"};
                model.Jobs = await imageRepo.GetProcessingJobs(Guid.Parse(identity.Identity.Name));

                return View["Home", model];

            });
        }
    }
}
