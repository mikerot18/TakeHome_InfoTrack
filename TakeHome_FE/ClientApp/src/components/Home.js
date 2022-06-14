import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            results: [],
            searchRunning: false,
            //Inputs
            keywords: "",
            url: "",
            resultsToCheck: 100,
            searchEngine: "Google",
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    static renderResults(results) {

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Result #</th>
                        <th>Location on Page</th>
                        <th>URL</th>
                    </tr>
                </thead>
                <tbody>
                    {results.map(result =>
                        <tr key={result.id}>
                            <td>{result.id}</td>
                            <td>{result.locationOnPage}</td>
                            <td> <a href={result.urlDecoded}>{result.urlDecoded}</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        this.collectData();
        event.preventDefault();
    }

    render() {
        let resultsRendered = (this.state.results.length > 0)
            ? Home.renderResults(this.state.results)
            : (this.state.searchRunning)
                ? <p style={{ color: 'red' }}>No matches found</p>
                : <p></p>;

  
        return (
            <form onSubmit={this.handleSubmit}>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Keywords</label>
                    <div class="col-md-10">
                        <input name="keywords" type="text" placeholder="efiling integration" value={this.state.keywords} onChange={this.handleInputChange} />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">URL</label>
                    <div class="col-md-10">
                        <input name="url" type="text" placeholder="www.infotrack.com" value={this.state.url} onChange={this.handleInputChange} />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label"># of Results to Check</label>
                    <div class="col-md-10">
                        <input name="resultsToCheck" type="number" value={this.state.resultsToCheck} onChange={this.handleInputChange} />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Search Engine</label>
                    <div class="col-md-3">
                        <select name="searchEngine" type="text" value={this.state.searchEngine} onChange={this.handleInputChange}>
                            <option selected value="0">Google</option>
                        </select>
                    </div>
                    <label class="col-md-7 col-form-label"></label>
                </div>
                <div class="form-group row">
                    <div class="col-sm-10">
                        <button type="submit" class="btn btn-primary">Search</button>
                    </div>
                </div>
                {resultsRendered}
            </form>
        );
    }

    async collectData() {
        const response = await fetch(`search?keywords=${encodeURIComponent(this.state.keywords)}&url=${encodeURIComponent(this.state.url)}&resultsToCheck=${encodeURIComponent(this.state.resultsToCheck)}&searchEngine=${encodeURIComponent(this.state.searchEngine)}`);
        const data = await response.json();
        this.setState({ results: data, searchRunning: true});
    }
}

