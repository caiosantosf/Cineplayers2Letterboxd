using Cineplayers2Letterboxd.Presentation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Cineplayers2Letterboxd.Infrastructure.Adapters;
using Cineplayers2Letterboxd.Domain.Interfaces;

var serviceCollection = new ServiceCollection()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .BuildServiceProvider();

var mediatr = new MediatrAdapter(serviceCollection.GetRequiredService<IMediator>());
var html = new HtmlAgilityPackAdapter();

await ConsoleMenu.Run(mediatr, html);