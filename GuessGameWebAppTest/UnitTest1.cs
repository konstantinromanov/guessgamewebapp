using GuessGameWebApp.Controllers;
using GuessGameWebApp.Models;
using GuessGameWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;


namespace GuessGameWebAppTest
{
    public class UnitTest1
    {

        private GameController _controller;
        private GuessGameWebApp.Models.IPlayerRepository _repository;


        [Fact]
        public void ControllerTest1()
        {
            _repository = InMemoryDb.GetInMemoryRepository();

            _controller = new GameController(_repository);
            var result = _controller.Index() as ViewResult;
            Assert.Equal("Index", result.ViewName);

            result = _controller.FirstScreen() as ViewResult;
            Assert.Equal("FirstScreen", result.ViewName);
        }
        [Fact]
        public void SControllerTest2()
        {
            _repository = InMemoryDb.GetInMemoryRepository();

            _controller = new GameController(_repository);

            var result = _controller.FirstScreen() as ViewResult;
            Assert.Equal("FirstScreen", result.ViewName);

        }
        //[Fact]
        //public void ControllerTest3()
        //{
        //    _repository = InMemoryDb.GetInMemoryRepository();

        //    _controller = new GameController(_repository);            
        //    var model = new FirstScreenInput() { Name = "Max" };


        //    var result = _controller.FirstScreen(model) as ViewResult;
        //    Assert.Equal("GameScreen", result.ViewName);

        //}

    }

}