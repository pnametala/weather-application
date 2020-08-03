import React, { Component } from 'react';
import axios from 'axios';
import './SearchHistory.css';

export class SearchHistory extends Component {
    
    constructor(props) {
        super(props);
    }
  
    
    render() {
        return (
            <div className="card search-history mt-4" key="search-history">
                <div className="card-body">
                    <h2 className="card-title text-center">Search History</h2>
                </div>
                <ul className="list-group list-group-flush">
                    {this.props.cities.map(city =>
                    <li className="list-group-item" key={['search',city.id]}>
                        <a href="#" onClick={(evt) => this.props.onClick(evt, city)}>{city.name} ({city.country})</a>
                    </li>
                    )}
                </ul>
            </div>
        );
    }
}
