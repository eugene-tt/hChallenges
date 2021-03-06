import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Counter from './components/Counter';
import Hello from './components/Hello';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/hello' component={Hello} />
    <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
</Layout>;
