import React, { Component } from 'react';
import {format}  from 'date-fns';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLongArrowAltDown, faLongArrowAltUp} from "@fortawesome/free-solid-svg-icons";

import './CurrentDisplay.css';


export class CurrentDisplay extends Component {
    
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
                <div className="card weather-current mt-4" key="current-skeleton">
                    <div className="card-body weather-current-body">
                        <h2 className="card-title text-center weather-current-city">Select a City</h2>
                    </div>
                    <div className="row no-gutters">
                        <div className="col-md-4">
                            <img src={``} className="card-img" />
                        </div>
                        <div className="col-md-8">
                            <div className="card-body">
                            </div>
                            <div className="card-body weather-min-max">
                            </div>
                        </div>
                    </div>
                </div>
            )
        }
        
        const city = this.props.city;
        const current = this.props.forecast.current;
        const today = this.props.forecast.daily[0];
        return (
            <div className="card weather-current mt-4" key="current-weather">
                <div className="card-body weather-current-body">
                    <h2 className="card-title text-center weather-current-city">{city.name} ({city.country})</h2>
                </div>
                <div className="row no-gutters">
                    <div className="col-md-5">
                        <img src={`http://openweathermap.org/img/wn/${current.weather[0].icon}@2x.png`} className="card-img" />
                    </div>
                    <div className="col-md-7">
                        <div className="card-body">
                            <h3 className="current-weather-day text-center">{this.timeConverter(current.dt)}</h3>
                            <h4 className="current-weather-day text-center">{current.weather[0].main}</h4>
                            <p className="font-weight-bold text-center">{Math.round(current.temperature)}°C</p>
                        </div>
                        <div className="card-body weather-min-max text-center">
                            <span className="weather-temp font-weight-bold"><FontAwesomeIcon className="text-danger" icon={faLongArrowAltDown} />{Math.round(today.temperature.min)}°C</span>
                            <span className="weather-temp font-weight-bold"><FontAwesomeIcon className="text-success" icon={faLongArrowAltUp} />{Math.round(today.temperature.max)}°C</span>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
