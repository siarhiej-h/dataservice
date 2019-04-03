import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Grid from './Grid';
import About from './About';

const Main = () => (
  <Switch>
    <Route exact path='/' component={Grid}/>
    <Route path='/About' component={About}/>
  </Switch>
)

export default Main
