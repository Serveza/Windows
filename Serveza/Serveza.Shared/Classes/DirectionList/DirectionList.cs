using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Services.Maps;

namespace Serveza.Classes.DirectionList
{
    public class DirectionList
    {
        public double dist;
        private double time;

        public List<Direction> list;
        private DirectionListViewModel _dlwm;
        public DirectionListViewModel dlwm
        {
            get { return _dlwm; }
            set
            {
                //   _blwm.detach();
                _dlwm = value;
                if (_dlwm != null)
                    _dlwm.list = new ObservableCollection<Direction>(list);
            }
        }

        public DirectionList(DirectionListViewModel view)
        {
            list = new List<Direction>();
            _dlwm = view;
        }

        public DirectionList()
        {
            list = new List<Direction>();
        }

        public void Add(Direction newDir)
        {
            list.Add(newDir);
            if (_dlwm != null)
                _dlwm.list = new ObservableCollection<Direction>(list);
        }
        public void Load(MapRouteFinderResult routeResult)
        {
            Clear();
            time = routeResult.Route.EstimatedDuration.TotalMinutes;
            dist = routeResult.Route.LengthInMeters / 1000;
            Direction tmp;
            foreach (MapRouteLeg leg in routeResult.Route.Legs)
            {
                foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                {
                    tmp = new Direction();
                    tmp.Load(maneuver);
                    Add(tmp);
                }
            }
            if (_dlwm != null)
                _dlwm.SetTimeAndDist(time, dist);
        }

        public void Clear()
        {
            list.Clear();
            if (_dlwm != null)
                _dlwm.list = new ObservableCollection<Direction>(list);
        }
    }
}
