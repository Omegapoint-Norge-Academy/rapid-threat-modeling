import { DefaultAzureCredential } from '@azure/identity';
import dotenv from 'dotenv';
import sql from 'mssql';
import logger from '../logger';

dotenv.config();

let pool: sql.ConnectionPool;

const getPool = async () => {
  logger.info('Getting pool');
  if (!pool) {
    logger.info('Creating new pool');
    if (process.env.NODE_ENV != 'prod') {
      logger.info('Using connection string');
      // Use connection string locally
      pool = await sql.connect(process.env.DB_CONNECTION_STRING!);
    } else {
      logger.info('Using azure credential');
      // Use managed identity in Azure (production)
      const credential = new DefaultAzureCredential();
      const { token } = await credential.getToken('https://database.windows.net');
      pool = await sql.connect({
        server: process.env.DB_SERVER!,
        database: process.env.DB_NAME!,
        authentication: {
          type: 'azure-active-directory-access-token',
          options: { token },
        },
        options: {
          encrypt: true,
        },
      });
    }
  } else {
    logger.info('Using existing pool');
  }
  return pool;
};

export const query = async <T = any>(sqlQuery: string, params?: any[]): Promise<T[]> => {
  const pool = await getPool();
  const result = await pool.request().query(sqlQuery);
  return result.recordset;
};
