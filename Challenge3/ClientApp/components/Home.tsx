import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as HomeState from '../store/Home';

type HomeProps =
    HomeState.HomeState        // ... state we've requested from the Redux store
    & typeof HomeState.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{  }>; 


class Home extends React.Component<HomeProps, {}> {
    componentWillMount()
    {
        this.props.requestServerTime();
    }

    public render() {
        return <div>
            <h1>Hello, world!</h1>

            <div className="alert redBox" role="alert">
                <h2 className="alert-heading">Todo</h2>
                <hr />
                <ul className="custom-bullet">
                    <li className="done-item">1: Add a menu item "Hello" to the left side and display</li>
                    <li className="done-item">2: Move the inline style of this red box to an external css file and try to make the box nicer</li>
                    <li className="done-item">3: Display the server time here: {this.props.serverTime}</li>
                </ul>
            </div>
            <p>Welcome to your new single-page application, built with:</p>
            <ul>
                <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                <li><a href='https://facebook.github.io/react/'>React</a>, <a href='http://redux.js.org'>Redux</a>, and <a href='http://www.typescriptlang.org/'>TypeScript</a> for client-side code</li>
                <li><a href='https://webpack.github.io/'>Webpack</a> for building and bundling client-side resources</li>
                <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
            </ul>
            <p>To help you get started, we've also set up:</p>
            <ul>
                <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
                <li><strong>Webpack dev middleware</strong>. In development mode, there's no need to run the <code>webpack</code> build tool. Your client-side resources are dynamically built on demand. Updates are available as soon as you modify any file.</li>
                <li><strong>Hot module replacement</strong>. In development mode, you don't even need to reload the page after making most changes. Within seconds of saving changes to files, rebuilt React components will be injected directly into your running application, preserving its live state.</li>
                <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and the <code>webpack</code> build tool produces minified static CSS and JavaScript files.</li>
                <li><strong>Server-side prerendering</strong>. To optimize startup time, your React application is first rendered on the server. The initial HTML and state is then transferred to the browser, where client-side code picks up where the server left off.</li>
            </ul>
        </div>;
    }
}

export default connect(
    (state: ApplicationState) => state.home, // Selects which state properties are merged into the component's props
    HomeState.actionCreators                 // Selects which action creators are merged into the component's props
)(Home) as typeof Home;
