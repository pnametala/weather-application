import React, { Component } from 'react';
import {format}  from 'date-fns';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLongArrowAltDown, faLongArrowAltUp} from "@fortawesome/free-solid-svg-icons";

import './ForecastReport.css';

export class ForecastReport extends Component {
    
    constructor(props) {
        super(props);
    }
  
    timeConverter(timestamp, displayTime = false) {
      const date = new Date(timestamp * 1000)
      return (displayTime) ? format(date, 'p') : format(date, 'cccc');
    }

    render() {
        if(this.props.isLoading) {
            const blocks = [0,1,2,3,4,5,6];
            return (
                <div className="card-group mt-4">
                    {blocks.map(daily =>
                        <div className="card" key={['forecast-skeleton', daily]}>
                            <div className="card-body">
                            </div>
                        </div>
                    )}
                </div>
            )
        }
        
        const forecast = this.props.forecast;
        return (
            <div className="card-group mt-4">
                {forecast.daily.map(daily =>
                    <div className="card" key={daily.dt}>
                        <div className="card-body">
                            <h5 className="card-title text-center">{this.timeConverter(daily.dt)}</h5>
                            <img src={`http://openweathermap.org/img/wn/${daily.weather[0].icon}@2x.png`} className="card-img-top" />
                            <div className="card-body weather-min-max text-center">
                                <span className="weather-temp"><FontAwesomeIcon className="text-danger" icon={faLongArrowAltDown} />{Math.round(daily.temperature.min)}°C</span>
                                <span className="weather-temp"><FontAwesomeIcon className="text-success" icon={faLongArrowAltUp} />{Math.round(daily.temperature.max)}°C</span>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        );
    }
}
