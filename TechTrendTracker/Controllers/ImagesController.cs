﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTrendTracker.Repositories.Interface;

namespace TechTrendTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //Call a repository

            var imageURL = await _imageRepository.UploadAsync(file);

            if (imageURL != null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return  new JsonResult(new {link = imageURL});
        }

    }
}
