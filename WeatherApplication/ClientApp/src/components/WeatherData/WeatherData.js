import React, { Component } from 'react';
import { Typeahead, withAsync } from 'react-bootstrap-typeahead';
import axios from 'axios';
import {format}  from 'date-fns';
import {Row, Col} from "reactstrap";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSearch} from "@fortawesome/free-solid-svg-icons";

import './WeatherData.css';
import {SearchHistory} from "./SearchHistory";
import {CurrentDisplay} from "./CurrentDisplay/CurrentDisplay";
import {ForecastReport} from "./ForecastReport/ForecastReport";
import {CurrentReport} from "./CurrentReport/CurrentReport";

const AsyncTypeahead = withAsync(Typeahead);

export class WeatherData extends Component {
    
    constructor(props) {
        super(props);
        this.state = { 
            isLoadingSearch: false,
            isLoadingWeather: true,
            cities: [],
            chosenCity: {},
            searchInput: [],
            forecast: {},
            history: []
        };
    }
  
    timeConverter(timestamp, displayTime = false) {
      const date = new Date(timestamp * 1000)
      return (displayTime) ? format(date, 'p') : format(date, 'cccc');
    }
  
    searchCities = (query) => {
      this.setState({isLoadingSearch: true});
      axios.get(`https://localhost:5001/api/Cities?query=${query}`)
        .then(res => {
            this.setState({
                isLoadingSearch: false,
                cities: res.data
            })
        })
        .catch(err => console.log(err));
    }
    
    onChange = (input) => {
        this.setState({searchInput: input})
    }

    loadHistoryCity = (evt, city) => {
        evt.persist();
        this.setState({chosenCity: city}, () => this.populateWeatherData(evt));
    }
    
    loadData = (evt) => {
        if (this.state.searchInput.length === 0) {
            evt.preventDefault();
            return;
        }
        console.log(this.state.searchInput)
        evt.persist();
        this.setState({chosenCity: this.state.searchInput[0]}, () => this.populateWeatherData(evt));
    }
    
    componentDidMount() {
        this.getSearchHistory();
    }
    
    populateWeatherData = (evt) => {
        evt.preventDefault();
        const city = this.state.chosenCity;
        
        this.setState({
            isLoadingWeather: true
        })
        axios.get(`https://localhost:5001/api/cities/${city.id}`)
            .then(() => {
                //successful result
                this.getSearchHistory();
                axios.get(`https://localhost:5001/api/weather/one-call/${city.coordinates.lat}/${city.coordinates.lon}`)
                    .then(res => {
                        this.setState( {
                            forecast: res.data,
                            isLoadingWeather: false
                        })
                    });
            });
    }

    getSearchHistory = () => {
        axios.get(`https://localhost:5001/api/cities/history`)
            .then(res => {
                this.setState( {
                    history: res.data
                })
            });
    }

    renderForecastsTable(forecast) {
        return (
            <React.Fragment>
                <section>
                    <Row>
                        <Col lg={3}>
                            <CurrentDisplay isLoading={this.state.isLoadingWeather} city={this.state.chosenCity} forecast={this.state.forecast}/>
                        </Col>
                        <Col lg={4}>
                            <CurrentReport isLoading={this.state.isLoadingWeather} forecast={this.state.forecast}/>
                        </Col>
                        <Col lg={3} className="offset-lg-2">
                            <SearchHistory onClick={this.loadHistoryCity} cities={this.state.history} />
                        </Col>
                    </Row>
                </section>
                <section>
                    <ForecastReport isLoading={this.state.isLoadingWeather} forecast={this.state.forecast} />
                </section>
            </React.Fragment>
        );
    }

    render() {
        let contents = this.renderForecastsTable(this.state.forecast);

        return (
            <React.Fragment>
                <form>
                    <div className="form-row d-flex justify-content-center">
                        <div className="col-lg-3">
                            <AsyncTypeahead
                                id="typeahead"
                                isLoading={this.state.isLoadingSearch}
                                labelKey={option => `${option.name} (${option.country})`}
                                placeholder="Search for a city..."
                                onSearch={this.searchCities}
                                onChange={this.onChange}
                                options={this.state.cities}
                                selected={this.state.searchInput}
                            />
                        </div>
                        <div className="col-lg-2">
                            <button className="btn btn-primary" onClick={this.loadData}><FontAwesomeIcon icon={faSearch}/></button>
                        </div>
                    </div>
                </form>
                {contents}
            </React.Fragment>
        );
    }
}
