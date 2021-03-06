﻿using Nancy;
using System.Collections.Generic;
using System.Linq;
using Nancy.ModelBinding;

namespace End.DragonAPI
{
	public class DragonModule : NancyModule
	{
		private static List<Dragon> _dragonList;

		private static object AddDragon(Dragon dragon)
		{
			if (dragon == null) return null;

			dragon.Id = _dragonList.Max(d => d.Id) + 1;
			_dragonList.Add(dragon);

			return new { dragon.Id };
		}

		private static Dragon GetDragon(int? id)
		{
			return _dragonList.FirstOrDefault(d => d.Id == id);
		}












		public DragonModule()
			: base("/dragon")
		{
			_dragonList = new List<Dragon>()
            {
                new Dragon(){ BreathesFire = true, Id = 1, Mass = 12000}
            };















			Get["/"] = _ => _dragonList;



















			Post["/"] = _ =>
			{
				var dragon = this.Bind<Dragon>(d => d.Id);

				return Response.AsJson(AddDragon(dragon));
			};
















			Get["/{id:int}"] = _ => GetDragon(_.id);
		}
	}
}