import React, { Component } from 'react';
import {format}  from 'date-fns';

import './CurrentReport.css';

export class CurrentReport extends Component {
    
    constructor(props) {
        super(props);
    }
  
    timeConverter(timestamp, displayTime = false) {
      const date = new Date(timestamp * 1000)
      return (displayTime) ? format(date, 'p') : format(date, 'cccc');
    }

    render() {
        if(this.props.isLoading) {
            return (
                <div className="card weather-current mt-4" key="current-report-skeleton">
                    <div className="card-body weather-current-body">
                        <h2 className="card-title text-center weather-current-city">Today's Forecast Report</h2>
                    </div>
                    <ul className="list-group list-group-flush">
                        <li className="list-group-item">
                            <span>Current time</span>
                        </li>
                        <li className="list-group-item">
                            <span>Sunrise</span>
                        </li>
                        <li className="list-group-item">
                            <span>Sunset</span>
                        </li>
                        <li className="list-group-item">
                            <span>Wind Speed</span>
                        </li>
                        <li className="list-group-item">
                            <span>Wind Degrees</span>
                        </li>
                    </ul>
                </div>
            )
        }
        
        const current = this.props.forecast.current;
        console.log(current)
        return (
            <div className="card weather-current mt-4" key={current.dt}>
                <div className="card-body weather-current-body">
                    <h2 className="card-title text-center weather-current-city">Today's Forecast Report</h2>
                </div>
                <ul className="list-group list-group-flush">
                    <li className="list-group-item d-flex justify-content-between">
                        <span>Current time</span><span>{this.timeConverter(current.dt, true)}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between">
                        <span>Sunrise</span><span>{this.timeConverter(current.sunrise, true)}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between">
                        <span>Sunset</span><span>{this.timeConverter(current.sunset, true)}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between">
                        <span>Wind Speed</span><span>{current.windSpeed}m/s</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between">
                        <span>Wind Degrees</span><span>{current.windDeg}°</span>
                    </li>
                </ul>
            </div>
        );
    }
}
