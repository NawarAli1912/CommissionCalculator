import React from 'react';
import './App.css';
import axios from 'axios';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';

function App() {
  const initialValues = {
    localSalesCount: '',
    foreignSalesCount: '',
    averageSaleAmount: '',
  };

  const validationSchema = Yup.object({
    localSalesCount: Yup.number()
      .typeError('Local Sales Count must be a number')
      .integer('Local Sales Count must be an integer')
      .min(0, 'Local Sales Count cannot be negative')
      .required('Local Sales Count is required'),
    foreignSalesCount: Yup.number()
      .typeError('Foreign Sales Count must be a number')
      .integer('Foreign Sales Count must be an integer')
      .min(0, 'Foreign Sales Count cannot be negative')
      .required('Foreign Sales Count is required'),
    averageSaleAmount: Yup.number()
      .typeError('Average Sale Amount must be a number')
      .min(0, 'Average Sale Amount cannot be negative')
      .required('Average Sale Amount is required'),
  });

  const [totalFcamaraCommission, setTotalFcamaraCommission] = React.useState(null);
  const [totalCompetitorCommission, setTotalCompetitorCommission] = React.useState(null);
  const [apiError, setApiError] = React.useState(null);

  const onSubmit = (values, { setSubmitting }) => {
    axios
      .post('https://localhost:5000/Commission', {
        localSalesCount: parseInt(values.localSalesCount),
        foreignSalesCount: parseInt(values.foreignSalesCount),
        averageSaleAmount: parseFloat(values.averageSaleAmount),
      })
      .then((response) => {
        setTotalFcamaraCommission(response.data.fCamaraCommissionAmount);
        setTotalCompetitorCommission(response.data.competitorCommissionAmount);
        setApiError(null);
      })
      .catch((error) => {
        console.error('There was an error calculating the commission!', error);
        setApiError('Failed to calculate commission. Please try again later.');
      })
      .finally(() => {
        setSubmitting(false);
      });
  };

  return (
    <div className="App">
      <header className="App-header">
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
          {({ isSubmitting }) => (
            <Form>
              <div>
                <label htmlFor="localSalesCount">Local Sales Count</label>
                <Field name="localSalesCount" type="text" />
                <ErrorMessage name="localSalesCount" component="div" className="error" />
              </div>

              <div>
                <label htmlFor="foreignSalesCount">Foreign Sales Count</label>
                <Field name="foreignSalesCount" type="text" />
                <ErrorMessage name="foreignSalesCount" component="div" className="error" />
              </div>

              <div>
                <label htmlFor="averageSaleAmount">Average Sale Amount</label>
                <Field name="averageSaleAmount" type="text" />
                <ErrorMessage name="averageSaleAmount" component="div" className="error" />
              </div>

              <button type="submit" disabled={isSubmitting}>
                {isSubmitting ? 'Calculating...' : 'Calculate'}
              </button>
            </Form>
          )}
        </Formik>
      </header>

      <div>
        <h3>Results</h3>
        {apiError && <p className="error">{apiError}</p>}
        {totalFcamaraCommission !== null && (
          <>
            <p>Total FCamara commission: £{totalFcamaraCommission.toFixed(2)}</p>
            <p>Total Competitor commission: £{totalCompetitorCommission.toFixed(2)}</p>
          </>
        )}
      </div>
    </div>
  );
}

export default App;