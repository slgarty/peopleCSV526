import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './pages/Home'
import Generate from './pages/Generate'
import Upload from './pages/Upload'


export default class App extends Component {

    render() {
        return (
           
                <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/Generate' component={Generate} />
                <Route exact path='/Upload' component={Upload} />
                </Layout>
        );
    }
}
