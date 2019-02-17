import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';


export default class Hello extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div className="jumbotron hello-jumbo">
          <h1 className="display-4">We love coding</h1>
            <p className="lead">Our passion is business efficiency enhancement for our clients, via smart application of automation techniques.</p>
          <hr className="my-4"/>
            <p>Our clients are companys looking to solve a business problem or create new possibilities through developing new software. They need an efficient software company to make it happen, one they can rely on.</p>
            <p>At Hyperio Software Ltd our tools allows us to work faster than our rivals. That shaves valuable time off your project and keeps costs low, whilst enhancing code quality, consistency and security.</p>
          <p className="lead">
            <a className="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
          </p>
        </div>;
    }
}
